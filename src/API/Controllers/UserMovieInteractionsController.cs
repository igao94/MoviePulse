using Application.UserMovieInteractions;
using Application.UserMovieInteractions.Commands.RateMovie;
using Application.UserMovieInteractions.Commands.RemoveRating;
using Application.UserMovieInteractions.Commands.ToggleMovieInWatchlist;
using Application.UserMovieInteractions.Commands.ToggleMovieWatchedStatus;
using Application.UserMovieInteractions.DTOs;
using Application.UserMovieInteractions.Queries.GetUserRatings;
using Application.UserMovieInteractions.Queries.GetWatchedMovies;
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

    [HttpGet("get-user-ratings")]
    public async Task<ActionResult<IEnumerable<UserMovieIntercationDto>>> GetUserRatings
        ([FromQuery] UserMovieIntercationParams userMovieIntercationParams)
    {
        return HandleResult(await Mediator.Send(new GetUserRatingsQuery(userMovieIntercationParams)));
    }

    [HttpPost("rate-movie")]
    public async Task<ActionResult> RateMovie(RateMovieDto rateMovieDto)
    {
        return HandleResult(await Mediator.Send(new RateMovieCommand(rateMovieDto)));
    }

    [HttpPut("remove-rating/{movieId}")]
    public async Task<ActionResult> RemoveRating(string movieId)
    {
        return HandleResult(await Mediator.Send(new RemoveRatingCommand(movieId)));
    }

    [HttpPut("mark-movie-as-watched/{movieId}")]
    public async Task<ActionResult> MarkMovieAsWatch(string movieId)
    {
        return HandleResult(await Mediator.Send(new ToggleMovieWatchedStatusCommand(movieId)));
    }

    [HttpGet("get-watched-movies")]
    public async Task<ActionResult<IEnumerable<UserMovieIntercationDto>>> GetWatchedMovies
        ([FromQuery] UserMovieIntercationParams userMovieIntercationParams)
    {
        return HandleResult(await Mediator.Send(new GetWatchedMoviesQuery(userMovieIntercationParams)));
    }
}
