using Application.Core;
using MediatR;

namespace Application.Watchlists.Commands.ToggleMovieInWatchlist;

public class ToggleMovieInWatchlistCommand(string movieId) : IRequest<Result<Unit>>
{
    public string MovieId { get; set; } = movieId;
}
