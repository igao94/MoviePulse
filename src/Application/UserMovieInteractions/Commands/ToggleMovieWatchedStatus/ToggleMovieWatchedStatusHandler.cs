using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.UserMovieInteractions.Commands.ToggleMovieWatchedStatus;

public class ToggleMovieWatchedStatusHandler(IUnitOfWork unitOfWork,
    IUserContext userContext) : IRequestHandler<ToggleMovieWatchedStatusCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(ToggleMovieWatchedStatusCommand request,
        CancellationToken cancellationToken)
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

        var userMovieInteraction = await unitOfWork.UserMovieInteractionRepository
            .GetUserMovieInteractionAsync(user.Id, movie.Id);

        if (userMovieInteraction is not null)
        {
            userMovieInteraction.IsWatched = !userMovieInteraction.IsWatched;
        }

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to mark movie.", 400);
    }
}
