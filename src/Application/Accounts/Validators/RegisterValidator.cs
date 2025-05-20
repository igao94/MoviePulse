using Application.Accounts.Commands.Register;
using Application.Accounts.DTOs;
using FluentValidation;

namespace Application.Accounts.Validators;

public class RegisterValidator : BaseUserAuthValidator<RegisterCommand, BaseAuthDto>
{
    public RegisterValidator() : base(x => x.RegisterDto)
    {
        RuleFor(x => x.RegisterDto.FirstName)
            .NotEmpty().WithMessage("FirstName is required.")
            .Matches(@"^[A-Z][a-z]+$")
            .WithMessage("First name must start with an uppercase letter and contain only one word.");

        RuleFor(x => x.RegisterDto.LastName)
            .NotEmpty().WithMessage("LastName is required")
            .Matches(@"^[A-Z][a-z]+$")
            .WithMessage("Last name must start with an uppercase letter and contain only one word.");

        RuleFor(x => x.RegisterDto.Username)
            .NotEmpty().WithMessage("Username is required.")
            .Matches(@"^[a-z0-9]+$")
            .WithMessage("Username must be a single word with only lowercase letters and numbers, without spaces.");
    }
}
