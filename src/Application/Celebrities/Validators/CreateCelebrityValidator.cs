using Application.Celebrities.Commands.CreateCelebrity;
using Application.Celebrities.DTOs;

namespace Application.Celebrities.Validators;

public class CreateCelebrityValidator
    : BaseCreateCelebrityValidator<CreateCelebrityCommand, BaseCreateCelebrityDto>
{
    public CreateCelebrityValidator() : base(c => c.CreateCelebrityDto)
    {

    }
}
