using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.UserMovieInteractions.Commands.RateMovie;

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

        await RateMovieAsync(user.Id, movie.Id, request.RateMovieDto.Rating);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to rate a movie.", 400);
    }

    private async Task RateMovieAsync(string userId, string movieId, int rating)
    {
        var movieRating = await unitOfWork.UserMovieInteractionRepository
            .GetUserMovieInteractionAsync(userId, movieId);

        if (movieRating is null)
        {
            movieRating = new UserMovieInteraction
            {
                UserId = userId,
                MovieId = movieId,
                Rating = rating,
                IsOnceRated = true,
                IsWatched = true,
                RatedAt = DateTime.UtcNow
            };

            unitOfWork.UserMovieInteractionRepository.AddMovieInteraction(movieRating);
        }
        else
        {
            movieRating.Rating = rating;

            movieRating.RatedAt = DateTime.UtcNow;
        }
    }
}
