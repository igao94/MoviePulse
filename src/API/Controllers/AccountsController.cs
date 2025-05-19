using Application.Accounts.Commands.Register;
using Application.Accounts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountsController : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<AccountDto>> RegisterUser(RegisterDto registerDto)
    {
        return HandleResult(await Mediator.Send(new RegisterCommand(registerDto)));
    }
}
