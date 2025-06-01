using Application.UserMovieInteractions;
using Application.UserMovieInteractions.Commands.ToggleMovieInWatchlist;
using Application.UserMovieInteractions.DTOs;
using Application.UserMovieInteractions.Queries.GetWatchlist;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UserMovieInteractionsController : BaseApiController
{
    [HttpPost("toggle-movie-in-watchlist/{movieId}")]
    public async Task<ActionResult> ToggleMovieInWatchlist(string movieId)
    {
        return HandleResult(await Mediator.Send(new ToggleMovieInWatchlistCommand(movieId)));
    }

    [HttpGet("get-watchlist")]
    public async Task<ActionResult<IEnumerable<UserMovieIntercationDto>>> GetWatchlist
        ([FromQuery] UserMovieIntercationParams userMovieIntercationParams)
    {
        return HandleResult(await Mediator.Send(new GetWatchlistQuery(userMovieIntercationParams)));
    }
}
