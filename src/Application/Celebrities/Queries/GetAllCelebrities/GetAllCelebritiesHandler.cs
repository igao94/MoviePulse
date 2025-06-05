using Application.Celebrities.DTOs;
using Application.Core;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Celebrities.Queries.GetAllCelebrities;

public class GetAllCelebritiesHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAllCelebritiesQuery, Result<PagedList<CelebrityDto, DateTime?>>>
{
    public async Task<Result<PagedList<CelebrityDto, DateTime?>>> Handle(GetAllCelebritiesQuery request,
        CancellationToken cancellationToken)
    {
        var (celebrities, nextCursor) = await unitOfWork.CelebrityRepository
            .GetAllCelebritiesAsync(request.CelebritySpecParams.Search,
                request.CelebritySpecParams.PageSize,
                request.CelebritySpecParams.Cursor);

        return Result<PagedList<CelebrityDto, DateTime?>>.Success(new PagedList<CelebrityDto, DateTime?>
        {
            Items = mapper.Map<IEnumerable<CelebrityDto>>(celebrities),
            NextCursor = nextCursor
        });
    }
}
