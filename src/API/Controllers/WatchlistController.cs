using Application.Watchlists.Commands.ToggleMovieInWatchlist;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class WatchlistController : BaseApiController
{
    [HttpPost("{movieId}")]
    public async Task<ActionResult> ToggleMovieInWatchlist(string movieId)
    {
        return HandleResult(await Mediator.Send(new ToggleMovieInWatchlistCommand(movieId)));
    }
}
