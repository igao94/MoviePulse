using Application.Celebrities.Commands.AddCelebrityToMovie;
using FluentValidation;

namespace Application.Celebrities.Validators;

public class AddCelebityToMovieValidator : AbstractValidator<AddCelebrityToMovieCommand>
{
    public AddCelebityToMovieValidator()
    {
        RuleFor(x => x.MovieId)
            .NotEmpty().WithMessage("MoveId is required.");        
        
        RuleFor(x => x.CelebrityId)
            .NotEmpty().WithMessage("CelebrityId is required.");

        RuleFor(x => x.RoleTypeIds)
            .NotEmpty()
            .Must(x => x.Any())
            .WithMessage("At least one RoleTypeId is required.");
    }
}
