using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;
using Domain.Entities;

namespace Application.Watchlists.Commands.ToggleMovieInWatchlist;

public class ToggleMovieInWatchlistHandler(IUnitOfWork unitOfWork,
    IUserContext userContext) : IRequestHandler<ToggleMovieInWatchlistCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(ToggleMovieInWatchlistCommand request,
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

        await ToggleMovieInWatchlistAsync(user.Id, movie.Id);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to save changes.", 400);
    }

    private async Task ToggleMovieInWatchlistAsync(string userId, string movieId)
    {
        var watchList = await unitOfWork.WatchlistRepository.GetWatchlistEntryAsync(userId, movieId);

        if (watchList is null)
        {
            watchList = new Watchlist
            {
                UserId = userId,
                MovieId = movieId
            };

            unitOfWork.WatchlistRepository.AddToWatchlist(watchList);
        }
        else
        {
            unitOfWork.WatchlistRepository.RemoveFromWatchlist(watchList);
        }
    }
}
