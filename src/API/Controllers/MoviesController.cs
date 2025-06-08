using Application.Core;
using Application.Movies;
using Application.Movies.Commands.AddGenreToMovie;
using Application.Movies.Commands.AddMoviePoster;
using Application.Movies.Commands.CreateMovie;
using Application.Movies.Commands.DeleteMovie;
using Application.Movies.Commands.DeleteMoviePoster;
using Application.Movies.Commands.UpdateMovie;
using Application.Movies.DTOs;
using Application.Movies.Queries.GetAllMovies;
using Application.Movies.Queries.GetMovieById;
using Application.Movies.Queries.GetMoviePosters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;

namespace API.Controllers;

public class MoviesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<PagedList<MovieDto, DateTime?>>> GetMovies
        ([FromQuery] MovieSpecParams movieSpecParams)
    {
        return HandleResult(await Mediator.Send(new GetAllMoviesQuery(movieSpecParams)));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto>> GetMovieById(string id)
    {
        return HandleResult(await Mediator.Send(new GetMovieByIdQuery(id)));
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpPost]
    public async Task<ActionResult<MovieDto>> CreateMovie(CreateMovieDto createMovieDto)
    {
        var result = await Mediator.Send(new CreateMovieCommand(createMovieDto));

        return HandleCreatedResult(result, nameof(GetMovieById), new { id = result.Value?.Id }, result.Value);
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateMovie(string id, UpdateMovieDto updateMovieDto)
    {
        updateMovieDto.Id = id;

        return HandleResult(await Mediator.Send(new UpdateMovieCommand(updateMovieDto)));
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMovie(string id)
    {
        return HandleResult(await Mediator.Send(new DeleteMovieComand(id)));
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpPost("add-genre-to-movie")]
    public async Task<ActionResult> AddGenreToMovie(AddGenreToMovieDto addGenreToMovieDto)
    {
        return HandleResult(await Mediator.Send(new AddGenreToMovieCommand(addGenreToMovieDto)));
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpPost("add-movie-poster")]
    public async Task<ActionResult> AddMoviePoster(AddMoviePosterCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpDelete("delete-movie-poster")]
    public async Task<ActionResult> DeleteMoviePoster(DeleteMoviePosterCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    [HttpGet("get-movie-posters/{id}")]
    public async Task<ActionResult<IEnumerable<PosterDto>>> GetMoviePosters(string id)
    {
        return HandleResult(await Mediator.Send(new GetMoviePostersQuery(id)));
    }
}
