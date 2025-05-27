using Application.Core;
using MediatR;

namespace Application.Celebrities.Commands.DeleteCelebrity;

public class DeleteCelebrityCommand(string id) : IRequest<Result<Unit>>
{
    public string Id { get; set; } = id;
}
