using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.UserMovieInteractions.Commands.ToggleMovieInWatchlist;

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
            return Result<Unit>.Failure("Movie not found.", 400);
        }

        await ToggleMovieInWatchlistAsync(user.Id, movie.Id);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to add movie to watchlist.", 400);
    }

    private async Task ToggleMovieInWatchlistAsync(string userId, string movieId)
    {
        var movieInteraction = await unitOfWork.UserMovieInteractionRepository
            .GetUserMovieInteractionAsync(userId, movieId);

        if (movieInteraction is null)
        {
            movieInteraction = new UserMovieInteraction
            {
                UserId = userId,
                MovieId = movieId,
                IsInWatchlist = true,
                AddedToWatchlistAt = DateTime.UtcNow
            };

            unitOfWork.UserMovieInteractionRepository.AddMovieInteraction(movieInteraction);

            return;
        }

        if (movieInteraction.IsInWatchlist)
        {
            movieInteraction.IsInWatchlist = false;

            movieInteraction.AddedToWatchlistAt = movieInteraction.IsInWatchlist ? DateTime.UtcNow : null;
        }
        else
        {
            movieInteraction.IsInWatchlist = true;

            movieInteraction.AddedToWatchlistAt = DateTime.UtcNow;
        }
    }
}
