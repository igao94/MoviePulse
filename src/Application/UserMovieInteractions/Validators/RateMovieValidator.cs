using Application.UserMovieInteractions.Commands.RateMovie;
using FluentValidation;

namespace Application.UserMovieInteractions.Validators;

public class RateMovieValidator : AbstractValidator<RateMovieCommand>
{
    public RateMovieValidator()
    {
        RuleFor(rm => rm.RateMovieDto.MovieId)
            .NotEmpty().WithMessage("MovieId is required.");

        RuleFor(rm => rm.RateMovieDto.Rating)
            .InclusiveBetween(1, 10).WithMessage("Rating must be whole number between 1 and 10.");
    }
}
