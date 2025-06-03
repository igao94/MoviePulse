using Application.Movies.Commands.AddGenreToMovie;
using FluentValidation;

namespace Application.Movies.Validators;

public class AddGenreToMovieValidator : AbstractValidator<AddGenreToMovieCommand>
{
    public AddGenreToMovieValidator()
    {
        RuleFor(g => g.AddGenreToMovieDto.MovieId)
            .NotEmpty().WithMessage("MovieId is required.");

        RuleFor(g => g.AddGenreToMovieDto.GenreId)
            .NotEmpty().WithMessage("GenreId is required.");
    }
}
