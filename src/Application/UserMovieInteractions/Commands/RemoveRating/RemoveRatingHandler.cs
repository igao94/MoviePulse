using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.UserMovieInteractions.Commands.RemoveRating;

public class RemoveRatingHandler(IUnitOfWork unitOfWork,
    IUserContext userContext) : IRequestHandler<RemoveRatingCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(RemoveRatingCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserByIdAsync(userContext.GetUserId());

        if (user is null)
        {
            return Result<Unit>.Failure("User not found.", 404);
        }

        var movie = await unitOfWork.MovieRepository.GetMovieByIdAsync(request.MovieId);

        if (movie is null)
        {
            return Result<Unit>.Failure("Movie not found.", 404);
        }

        var movieInteraction = await unitOfWork.UserMovieInteractionRepository
            .GetUserMovieInteractionAsync(user.Id, movie.Id);

        if (movieInteraction is not null)
        {
            movieInteraction.Rating = null;

            movieInteraction.RatedAt = null;
        }

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to remove rating.", 400);
    }
}
