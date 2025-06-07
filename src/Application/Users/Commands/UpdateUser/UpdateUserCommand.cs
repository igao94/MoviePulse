using Application.Core;
using Application.Users.DTOs;
using MediatR;

namespace Application.Users.Commands.UpdateUser;

public class UpdateUserCommand(UpdateUserDto updateUserDto) : IRequest<Result<Unit>>
{
    public UpdateUserDto UpdateUserDto { get; set; } = updateUserDto;
}
