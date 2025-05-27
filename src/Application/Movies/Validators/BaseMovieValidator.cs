using Application.Extensions;
using Application.Movies.DTOs;
using FluentValidation;

namespace Application.Movies.Validators;

public class BaseMovieValidator<T, TDto> : AbstractValidator<T> where TDto : BaseMovieDto
{
    public BaseMovieValidator(Func<T, TDto> selector)
    {
        RuleFor(x => selector(x).Title)
            .NotEmpty().WithMessage("Title is required.");

        RuleFor(x => selector(x).Description)
            .NotEmpty().WithMessage("Description is required.");

        RuleFor(x => selector(x).ReleaseDate)
            .NotEmpty().WithMessage("RelaseDate is required")
            .Must(x => x.BeValidDate())
            .WithMessage("Date must be in format 'yyyy-MM-dd', for example 2025-05-17.");

        RuleFor(x => selector(x).DurationInMinutes)
            .NotEmpty().WithMessage("DurationInMinutes is required.");

        RuleFor(x => selector(x).Rating)
            .NotEmpty().WithMessage("Rating is required.");
    }
}
