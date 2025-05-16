using Application.Movies.DTOs;
using Application.Movies.GetAllMovies;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MoviesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
    {
        return HandleResult(await Mediator.Send(new GetAllMoviesQuery()));
    }
}
