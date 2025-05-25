using Application.Celebrities.Commands.AddCelebrityToMovie;
using Application.Celebrities.Commands.CreateCelebrity;
using Application.Celebrities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;

namespace API.Controllers;

public class CelebritiesController : BaseApiController
{
    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpPost]
    public async Task<ActionResult<CelebrityDto>> CreateCelebrity(CreateCelebrityDto createCelebrityDto)
    {
        return HandleResult(await Mediator.Send(new CreateCelebrityCommand(createCelebrityDto)));
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpPost("add-celebrity-to-movie")]
    public async Task<ActionResult> AddCelebrityToMovie(AddCelebrityToMovieCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }
}
