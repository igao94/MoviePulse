using Application.Users.Command.UpdateUser;
using FluentValidation;

namespace Application.Users.Validators;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.UpdateUserDto.FirstName)
            .NotEmpty().WithMessage("FirstName is required.")
            .Matches(@"^[A-Z][a-z]+$")
            .WithMessage("First name must start with an uppercase letter and contain only one word.");

        RuleFor(x => x.UpdateUserDto.LastName)
            .NotEmpty().WithMessage("LastName is required")
            .Matches(@"^[A-Z][a-z]+$")
            .WithMessage("Last name must start with an uppercase letter and contain only one word.");
    }
}
