using Application.Celebrities.DTOs;
using Application.Core;
using MediatR;

namespace Application.Celebrities.Queries.GetCelebrityById;

public class GetCelebrityByIdQuery(string id) : IRequest<Result<CelebrityDto>>
{
    public string Id { get; set; } = id;
}
