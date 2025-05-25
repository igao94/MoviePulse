using Application.Celebrities.DTOs;
using Application.Core;
using MediatR;

namespace Application.Celebrities.Commands.CreateCelebrity;

public class CreateCelebrityCommand(CreateCelebrityDto createCelebrityDto) : IRequest<Result<CelebrityDto>>
{
    public CreateCelebrityDto CreateCelebrityDto { get; set; } = createCelebrityDto;
}
