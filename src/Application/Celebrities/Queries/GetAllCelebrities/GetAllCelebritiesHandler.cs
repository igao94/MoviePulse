using Application.Celebrities.DTOs;
using Application.Core;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Celebrities.Queries.GetAllCelebrities;

public class GetAllCelebritiesHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAllCelebritiesQuery, Result<IEnumerable<CelebrityDto>>>
{
    public async Task<Result<IEnumerable<CelebrityDto>>> Handle(GetAllCelebritiesQuery request,
        CancellationToken cancellationToken)
    {
        var celebrities = await unitOfWork.CelebrityRepository
            .GetAllCelebritiesAsync(request.CelebritySpecParams.Search, request.CelebritySpecParams.Sort);

        return Result<IEnumerable<CelebrityDto>>.Success(mapper.Map<IEnumerable<CelebrityDto>>(celebrities));
    }
}
