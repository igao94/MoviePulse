using Application.Users.Commands.AddPhoto;
using Application.Users.Commands.DeletePhoto;
using Application.Users.Commands.DeleteUser;
using Application.Users.Commands.UpdateUser;
using Application.Users.DTOs;
using Application.Users.Queries.GetUserById;
using Application.Users.Queries.GetUserPhotos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController : BaseApiController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUserById(string id)
    {
        return HandleResult(await Mediator.Send(new GetUserByIdQuery(id)));
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser(UpdateUserDto updateUserDto)
    {
        return HandleResult(await Mediator.Send(new UpdateUserCommand(updateUserDto)));
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteUser()
    {
        return HandleResult(await Mediator.Send(new DeleteUserCommand()));
    }

    [HttpPost("add-photo")]
    public async Task<ActionResult> AddPhoto([FromForm] AddPhotoCommand command)
    {
        return HandleResult(await Mediator.Send(command));
    }

    [HttpDelete("delete-photo/{photoId}")]
    public async Task<ActionResult> DeletePhoto(string photoId)
    {
        return HandleResult(await Mediator.Send(new DeletePhotoCommand(photoId)));
    }

    [HttpGet("get-user-photos")]
    public async Task<ActionResult<IEnumerable<PhotoDto>>> GetUserPhotos()
    {
        return HandleResult(await Mediator.Send(new GetUserPhotosQuery()));
    }
}
