using Application.Celebrities.Commands.CreateCelebrity;
using Application.Extensions;
using FluentValidation;

namespace Application.Celebrities.Validators;

public class CreateCelebrityValidator : AbstractValidator<CreateCelebrityCommand>
{
    public CreateCelebrityValidator()
    {
        RuleFor(c => c.CreateCelebrityDto.FirstName)
            .NotEmpty().WithMessage("FirstName is required.")
            .Matches(@"^(?:[A-Z][a-z]*\s*)+$")
            .WithMessage("First name must start with an uppercase letter and contain only words starting with uppercase letters.");

        RuleFor(c => c.CreateCelebrityDto.LastName)
            .NotEmpty().WithMessage("LastName is required")
            .Matches(@"^(?:[A-Z][a-z]*\s*)+$")
            .WithMessage("Last name must start with an uppercase letter and contain only words starting with uppercase letters.");

        RuleFor(c => c.CreateCelebrityDto.Bio)
            .NotEmpty().WithMessage("Bio is required.");

        RuleFor(c => c.CreateCelebrityDto.DateOfBirth)
            .NotEmpty().WithMessage("DateOfBirth is required.")
            .Must(c => c.BeValidDate())
            .WithMessage("Date must be in format 'yyyy-MM-dd', for example 2025-05-25.");
    }
}
