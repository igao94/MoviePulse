using Application.Celebrities.DTOs;
using Application.Core;
using MediatR;

namespace Application.Celebrities.Queries.GetAllCelebrities;

public class GetAllCelebritiesQuery(CelebritySpecParams celebritySpecParams)
    : IRequest<Result<PagedList<CelebrityDto, DateTime?>>>
{
    public CelebritySpecParams CelebritySpecParams { get; set; } = celebritySpecParams;
}
