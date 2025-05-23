using Application.Users.Command.DeleteUser;
using Application.Users.Command.UpdateUser;
using Application.Users.DTOs;
using Application.Users.Queries.GetUserById;
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
}
