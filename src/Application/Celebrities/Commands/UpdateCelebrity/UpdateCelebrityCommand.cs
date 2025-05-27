using Application.Celebrities.DTOs;
using Application.Core;
using MediatR;

namespace Application.Celebrities.Commands.UpdateCelebrity;

public class UpdateCelebrityCommand(UpdateCelebrityDto updateCelebrityDto) : IRequest<Result<Unit>>
{
    public UpdateCelebrityDto UpdateCelebrityDto { get; set; } = updateCelebrityDto;
}
