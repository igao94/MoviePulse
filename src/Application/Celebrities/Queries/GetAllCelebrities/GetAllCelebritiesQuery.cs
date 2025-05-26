using Application.Celebrities.DTOs;
using Application.Core;
using MediatR;

namespace Application.Celebrities.Queries.GetAllCelebrities;

public class GetAllCelebritiesQuery : IRequest<Result<IEnumerable<CelebrityDto>>>
{

}
