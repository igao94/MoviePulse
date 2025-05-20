using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    private IMediator? _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess && result.Value is not null)
        {
            return Ok(result.Value);
        }

        if (!result.IsSuccess && result.StatusCode == 404)
        {
            return NotFound();
        }

        return BadRequest(result.Error);
    }

    protected ActionResult HandleCreatedResult<T>(Result<T> result,
        string? actionName,
        object? routeValues,
        object? value)
    {
        if (!result.IsSuccess && result.StatusCode == 404)
        {
            return NotFound();
        }

        if (result.IsSuccess && result.Value is not null)
        {
            return CreatedAtAction(actionName, routeValues, value);
        }

        return BadRequest(result.Error);
    }
}
