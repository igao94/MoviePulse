using Application.Core;
using MediatR;

namespace Application.Watchlists.Commands.ToggleMovieWatchedStatus;

public class ToggleMovieWatchedStatusCommand(string movieId) : IRequest<Result<Unit>>
{
    public string MovieId { get; set; } = movieId;
}
