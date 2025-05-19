using Application.Accounts.DTOs;
using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Interfaces;

namespace Application.Accounts.Commands.Register;

public class RegisterHandler(IUnitOfWork unitOfWork,
    IHmacPasswordHasher hmacPasswordHasher,
    ITokenService tokenService) : IRequestHandler<RegisterCommand, Result<AccountDto>>
{
    public async Task<Result<AccountDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var (hash, salt) = hmacPasswordHasher.HashPassword(request.RegisterDto.Password);

        var user = new User
        {
            Username = request.RegisterDto.Username,
            Email = request.RegisterDto.Email,
            FirstName = request.RegisterDto.FirstName,
            LastName = request.RegisterDto.LastName,
            PasswordHash = hash,
            PasswordSalt = salt
        };

        unitOfWork.AccountRepository.RegisterUser(user);

        var result = await unitOfWork.SaveChangesAsync();

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
            : Result<AccountDto>.Failure("Failed to register user.", 400);
    }
}
