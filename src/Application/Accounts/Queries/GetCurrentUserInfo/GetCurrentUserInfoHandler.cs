using Application.Accounts.DTOs;
using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Accounts.Queries.GetCurrentUserInfo;

public class GetCurrentUserInfoHandler(IUnitOfWork unitOfWork,
    IUserContext userContext) : IRequestHandler<GetCurrentUserInfoQuery, Result<CurrentUserDto>>
{
    public async Task<Result<CurrentUserDto>> Handle(GetCurrentUserInfoQuery request,
        CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserByIdAsync(userContext.GetUserId());

        if (user is null)
        {
            return Result<CurrentUserDto>.Failure("Please log in.", 400);
        }

        return Result<CurrentUserDto>.Success(new CurrentUserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
        });
    }
}
