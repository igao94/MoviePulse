using Application.Accounts.DTOs;
using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Shared.Constants;
using Shared.Interfaces;

namespace Application.Accounts.Commands.Register;

public class RegisterHandler(IUnitOfWork unitOfWork,
    IHmacPasswordHasher hmacPasswordHasher,
    ITokenService tokenService) : IRequestHandler<RegisterCommand, Result<AccountDto>>
{
    public async Task<Result<AccountDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await unitOfWork.AccountRepository.IsUsernameTakenAsync(request.RegisterDto.Username))
        {
            return Result<AccountDto>.Failure("Username is alredy taken.", 400);
        }

        if (await unitOfWork.AccountRepository.IsEmailTakenAsync(request.RegisterDto.Email))
        {
            return Result<AccountDto>.Failure("Email is alredy taken.", 400);
        }

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

        unitOfWork.AccountRepository.AddUser(user);

        unitOfWork.RoleRepository.AddUserToRole(user.Id, UserRoles.MemberRoleId);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<AccountDto>.Success(new AccountDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = await tokenService.GetTokenAsync(user)
            })
            : Result<AccountDto>.Failure("Failed to register user.", 400);
    }
}
