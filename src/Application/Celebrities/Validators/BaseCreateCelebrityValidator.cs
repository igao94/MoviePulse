using Application.Celebrities.DTOs;
using Application.Extensions;
using FluentValidation;

namespace Application.Celebrities.Validators;

public class BaseCreateCelebrityValidator<T, TDto> : AbstractValidator<T> where TDto : BaseCreateCelebrityDto
{
    public BaseCreateCelebrityValidator(Func<T, TDto> selector)
    {
        RuleFor(c => selector(c).FirstName)
            .NotEmpty().WithMessage("FirstName is required.")
            .Matches(@"^(?:[A-Z][a-z]*\s*)+$")
            .WithMessage("First name must start with an uppercase letter and contain only words starting with uppercase letters.");

        RuleFor(c => selector(c).LastName)
            .NotEmpty().WithMessage("LastName is required")
            .Matches(@"^(?:[A-Z][a-z]*\s*)+$")
            .WithMessage("Last name must start with an uppercase letter and contain only words starting with uppercase letters.");

        RuleFor(c => selector(c).Bio)
            .NotEmpty().WithMessage("Bio is required.");

        RuleFor(c => selector(c).DateOfBirth)
            .NotEmpty().WithMessage("DateOfBirth is required.")
            .Must(c => c.BeValidDate())
            .WithMessage("Date must be in format 'yyyy-MM-dd', for example 2025-05-25.");
    }
}
