using Application.Accounts.DTOs;
using FluentValidation;

namespace Application.Accounts.Validators;

public class BaseUserAuthValidator<T, TDto> : AbstractValidator<T> where TDto : UserAuthDto
{
    public BaseUserAuthValidator(Func<T, TDto> selector)
    {
        RuleFor(x => selector(x).Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Please enter a valid email address.");

        RuleFor(x => selector(x).Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
            .MaximumLength(20).WithMessage("Password cannot exceed 20 characters.")
            .Matches(@"[\W_]").WithMessage("Password must contain at least one non-alphanumeric character.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one digit.");
    }
}
