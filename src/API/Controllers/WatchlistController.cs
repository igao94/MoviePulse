using Application.Watchlists;
using Application.Watchlists.Commands.ToggleMovieInWatchlist;
using Application.Watchlists.DTOs;
using Application.Watchlists.Queries.GetWatchlist;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class WatchlistController : BaseApiController
{
    [HttpPost("{movieId}")]
    public async Task<ActionResult> ToggleMovieInWatchlist(string movieId)
    {
        return HandleResult(await Mediator.Send(new ToggleMovieInWatchlistCommand(movieId)));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WatchlistDto>>> GetWatchlist
        ([FromQuery] WatchlistParams watchlistParams)
    {
        return HandleResult(await Mediator.Send(new GetWatchlistQuery(watchlistParams)));
    }
}
