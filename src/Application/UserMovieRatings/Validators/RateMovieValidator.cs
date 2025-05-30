using Application.UserMovieRatings.Commands.RateMovie;
using FluentValidation;

namespace Application.UserMovieRatings.Validators;

public class RateMovieValidator : AbstractValidator<RateMovieCommand>
{
    public RateMovieValidator()
    {
        RuleFor(rm => rm.RateMovieDto.MovieId)
            .NotEmpty().WithMessage("MovieId is required.");

        RuleFor(rm => rm.RateMovieDto.Rating)
            .InclusiveBetween(1, 10).WithMessage("Rating must be a whole number between 1 and 10.");
    }
}
