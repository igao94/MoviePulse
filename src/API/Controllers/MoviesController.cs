using Application.Movies.DTOs;
using Application.Movies.GetAllMovies;
using Application.Movies.GetMovieById;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MoviesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
    {
        return HandleResult(await Mediator.Send(new GetAllMoviesQuery()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto>> GetMovieById(string id)
    {
        return HandleResult(await Mediator.Send(new GetMovieByIdQuery(id)));
    }
}
