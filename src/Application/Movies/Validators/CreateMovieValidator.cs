using Application.Movies.Commands.CreateMovie;
using Application.Movies.DTOs;

namespace Application.Movies.Validators;

public class CreateMovieValidator : BaseMovieValidator<CreateMovieCommand, CreateMovieDto>
{
    public CreateMovieValidator() : base(x => x.CreateMovieDto)
    {

    }
}
