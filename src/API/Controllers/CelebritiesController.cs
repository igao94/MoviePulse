using Application.Celebrities;
using Application.Celebrities.Commands.AddCelebrityToMovie;
using Application.Celebrities.Commands.CreateCelebrity;
using Application.Celebrities.Commands.DeleteCelebrity;
using Application.Celebrities.Commands.UpdateCelebrity;
using Application.Celebrities.DTOs;
using Application.Celebrities.Queries.GetAllCelebrities;
using Application.Celebrities.Queries.GetCelebrityById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;

namespace API.Controllers;

public class CelebritiesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CelebrityDto>>> GetAllCelebrities(
        [FromQuery] CelebritySpecParams celebritySpecParams)
    {
        return HandleResult(await Mediator.Send(new GetAllCelebritiesQuery(celebritySpecParams)));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CelebrityDto>> GetCelebrityById(string id)
    {
        return HandleResult(await Mediator.Send(new GetCelebrityByIdQuery(id)));
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpPost]
    public async Task<ActionResult<CelebrityDto>> CreateCelebrity(CreateCelebrityDto createCelebrityDto)
    {
        var result = await Mediator.Send(new CreateCelebrityCommand(createCelebrityDto));

        return HandleCreatedResult(result, nameof(GetCelebrityById), new { result.Value?.Id }, result.Value);
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpPost("add-celebrity-to-movie")]
    public async Task<ActionResult> AddCelebrityToMovie(AddCelebrityToMovieDto addCelebrityToMovieDto)
    {
        return HandleResult(await Mediator.Send(new AddCelebrityToMovieCommand(addCelebrityToMovieDto)));
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCelebrity(string id)
    {
        return HandleResult(await Mediator.Send(new DeleteCelebrityCommand(id)));
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCelebrity(string id, UpdateCelebrityDto updateCelebrityDto)
    {
        updateCelebrityDto.Id = id;

        return HandleResult(await Mediator.Send(new UpdateCelebrityCommand(updateCelebrityDto)));
    }
}
