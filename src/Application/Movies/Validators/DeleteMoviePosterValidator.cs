using Application.Movies.Commands.DeleteMoviePoster;
using FluentValidation;

namespace Application.Movies.Validators;

public class DeleteMoviePosterValidator : AbstractValidator<DeleteMoviePosterCommand>
{
    public DeleteMoviePosterValidator()
    {
        RuleFor(mp => mp.MovieId)
            .NotEmpty().WithMessage("MovieId is required.");

        RuleFor(mp => mp.PosterId)
            .NotEmpty().WithMessage("PosterId is required.");
    }
}
