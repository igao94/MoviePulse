using Application.Celebrities.Commands.AddCelebrityToMovie;
using FluentValidation;

namespace Application.Celebrities.Validators;

public class AddCelebityToMovieValidator : AbstractValidator<AddCelebrityToMovieCommand>
{
    public AddCelebityToMovieValidator()
    {
        RuleFor(x => x.AddCelebrityToMovieDto.MovieId)
            .NotEmpty().WithMessage("MoveId is required.");

        RuleFor(x => x.AddCelebrityToMovieDto.CelebrityId)
            .NotEmpty().WithMessage("CelebrityId is required.");

        RuleFor(x => x.AddCelebrityToMovieDto.RoleTypeIds)
            .NotEmpty().WithMessage("At least one RoleTypeId is required.");
    }
}
