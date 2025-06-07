using Application.Core;
using Application.Interfaces;
using Application.Users.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Queries.GetUserPhotos;

public class GetUserPhotosHandler(IUnitOfWork unitOfWork,
    IUserContext userContext,
    IMapper mapper) : IRequestHandler<GetUserPhotosQuery, Result<IEnumerable<PhotoDto>>>
{
    public async Task<Result<IEnumerable<PhotoDto>>> Handle(GetUserPhotosQuery request,
        CancellationToken cancellationToken)
    {
        var photos = await unitOfWork.UserRepository.GetUserPhotosAsync(userContext.GetUserId());

        return Result<IEnumerable<PhotoDto>>.Success(mapper.Map<IEnumerable<PhotoDto>>(photos));
    }
}
