﻿using Application.Accounts.Commands.Login;
using Application.Accounts.DTOs;

namespace Application.Accounts.Validators;

public class LoginValidator : BaseUserAuthValidator<LoginCommand, BaseAuthDto> 
{
    public LoginValidator() : base(x => x.LoginDto)
    {
        
    }
}
