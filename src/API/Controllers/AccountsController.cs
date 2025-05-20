using Application.Accounts.Commands.Login;
using Application.Accounts.Commands.Register;
using Application.Accounts.DTOs;
using Application.Accounts.Queries.GetCurrentUserInfo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[AllowAnonymous]
public class AccountsController : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<AccountDto>> RegisterUser(RegisterDto registerDto)
    {
        return HandleResult(await Mediator.Send(new RegisterCommand(registerDto)));
    }

    [HttpPost("login")]
    public async Task<ActionResult<AccountDto>> Login(LoginDto loginDto)
    {
        return HandleResult(await Mediator.Send(new LoginCommand(loginDto)));
    }

    [HttpGet("get-current-user-info")]
    public async Task<ActionResult<CurrentUserDto>> GetCurrentUserInfo()
    {
        return HandleResult(await Mediator.Send(new GetCurrentUserInfoQuery()));
    }
}
