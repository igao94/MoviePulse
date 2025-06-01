using Application.UserMovieInteractions.Commands.ToggleMovieInWatchlist;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UserMovieInteractionsController : BaseApiController
{
    [HttpPost("toggle-movie-in-watchlist/{movieId}")]
    public async Task<ActionResult> ToggleMovieInWatchlist(string movieId)
    {
        return HandleResult(await Mediator.Send(new ToggleMovieInWatchlistCommand(movieId)));
    }
}
