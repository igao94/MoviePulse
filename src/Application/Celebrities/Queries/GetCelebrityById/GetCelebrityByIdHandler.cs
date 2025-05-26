using Application.Celebrities.DTOs;
using Application.Core;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Celebrities.Queries.GetCelebrityById;

public class GetCelebrityByIdHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetCelebrityByIdQuery, Result<CelebrityDto>>
{
    public async Task<Result<CelebrityDto>> Handle(GetCelebrityByIdQuery request,
        CancellationToken cancellationToken)
    {
        var celebrity = await unitOfWork.CelebrityRepository.GetCelebrityByIdAsync(request.Id);

        if (celebrity is null)
        {
            return Result<CelebrityDto>.Failure("Celebrity not found.", 404);
        }

        return Result<CelebrityDto>.Success(mapper.Map<CelebrityDto>(celebrity));
    }
}
