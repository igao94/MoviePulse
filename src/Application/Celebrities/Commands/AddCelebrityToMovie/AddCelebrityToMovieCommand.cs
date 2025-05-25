using Application.Celebrities.DTOs;
using Application.Core;
using MediatR;

namespace Application.Celebrities.Commands.AddCelebrityToMovie;

public class AddCelebrityToMovieCommand(string movieId,
    string celebrityId,
    IEnumerable<string> roleTypeIds) : IRequest<Result<Unit>>
{
    public string MovieId { get; set; } = movieId;
    public string CelebrityId { get; set; } = celebrityId;
    public IEnumerable<string> RoleTypeIds { get; set; } = roleTypeIds;
}
