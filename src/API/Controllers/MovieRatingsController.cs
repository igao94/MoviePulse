using Application.UserMovieRatings.Commands.RateMovie;
using Application.UserMovieRatings.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MovieRatingsController : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult> RateMovie(RateMovieDto rateMovieDto)
    {
        return HandleResult(await Mediator.Send(new RateMovieCommand(rateMovieDto)));
    }
}
