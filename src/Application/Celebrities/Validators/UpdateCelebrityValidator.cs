using Application.Celebrities.Commands.UpdateCelebrity;
using Application.Celebrities.DTOs;

namespace Application.Celebrities.Validators;

public class UpdateCelebrityValidator
    : BaseCreateCelebrityValidator<UpdateCelebrityCommand, BaseCreateCelebrityDto>
{
    public UpdateCelebrityValidator() : base(uc => uc.UpdateCelebrityDto)
    {

    }
}
