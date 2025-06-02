using Application.UserMovieInteractions;
using Application.UserMovieInteractions.Commands.RateMovie;
using Application.UserMovieInteractions.Commands.ToggleMovieInWatchlist;
using Application.UserMovieInteractions.Commands.ToggleMovieWatchedStatus;
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

    [HttpPut("mark-movie-as-watched/{movieId}")]
    public async Task<ActionResult> MarkMovieAsWatch(string movieId)
    {
        return HandleResult(await Mediator.Send(new ToggleMovieWatchedStatusCommand(movieId)));
    }

    [HttpGet("get-watchlist")]
    public async Task<ActionResult<IEnumerable<UserMovieIntercationDto>>> GetWatchlist
        ([FromQuery] UserMovieIntercationParams userMovieIntercationParams)
    {
        return HandleResult(await Mediator.Send(new GetWatchlistQuery(userMovieIntercationParams)));
    }

    [HttpPost("rate-movie")]
    public async Task<ActionResult> RateMovie(RateMovieDto rateMovieDto)
    {
        return HandleResult(await Mediator.Send(new RateMovieCommand(rateMovieDto)));
    }
}
