using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Watchlists.Commands.ToggleMovieWatchedStatus;

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
            return Result<Unit>.Failure("Movie not found.", 400);
        }

        var toggleResult = await ToggleMovieWatchedStatusAsync(user.Id, movie.Id);

        if (!toggleResult)
        {
            return Result<Unit>.Failure("Movie is not on watchlist.", 400);
        }

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to mark movie.", 400);
    }

    private async Task<bool> ToggleMovieWatchedStatusAsync(string userId, string movieId)
    {
        var watchlist = await unitOfWork.WatchlistRepository.GetWatchlistEntryAsync(userId, movieId);

        if (watchlist is null)
        {
            return false;
        }

        watchlist.IsWatched = !watchlist.IsWatched;

        return true;
    }
}
