using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.UserMovieRatings.Commands.RateMovie;

public class RateMovieHandler(IUnitOfWork unitOfWork,
    IUserContext userContext) : IRequestHandler<RateMovieCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(RateMovieCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserByIdAsync(userContext.GetUserId());

        if (user is null)
        {
            return Result<Unit>.Failure("User not found.", 404);
        }

        var movie = await unitOfWork.MovieRepository.GetMovieByIdAsync(request.RateMovieDto.MovieId);

        if (movie is null)
        {
            return Result<Unit>.Failure("Movie not found.", 404);
        }

        await AddUserMovieRatingAsync(user.Id, movie.Id, request.RateMovieDto.Rating);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to rate a movie.", 400);
    }

    private async Task AddUserMovieRatingAsync(string userId, string movieId, int rating)
    {
        var userMovieRating = await unitOfWork.UserMovieRatingRepository
            .GetUserMovieRatingAsync(userId, movieId);

        if (userMovieRating is null)
        {
            userMovieRating = new UserMovieRating
            {
                UserId = userId,
                MovieId = movieId,
                Rating = rating
            };

            unitOfWork.UserMovieRatingRepository.AddUserMovieRating(userMovieRating);
        }
        else
        {
            userMovieRating.Rating = rating;
        }
    }
}
