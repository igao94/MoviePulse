using Application.Accounts.DTOs;
using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;
using Shared.Interfaces;

namespace Application.Accounts.Commands.Login;

public class LoginHandler(IUnitOfWork unitOfWork,
    IHmacPasswordHasher hmacPasswordHasher,
    ITokenService tokenService) : IRequestHandler<LoginCommand, Result<AccountDto>>
{
    public async Task<Result<AccountDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.AccountRepository.GetUserByEmailAsync(request.LoginDto.Email);

        if (user is null)
        {
            return Result<AccountDto>.Failure("Invalid credentials.", 400);
        }

        var result = hmacPasswordHasher
            .VerifyPassword(user.PasswordHash, user.PasswordSalt, request.LoginDto.Password);

        return result
            ? Result<AccountDto>.Success(new AccountDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenService.GetToken(user)
            })
            : Result<AccountDto>.Failure("Invalid credentials.", 400);
    }
}
