using Application.Core;
using Application.Users.DTOs;
using MediatR;

namespace Application.Users.Queries.GetUserById;

public class GetUserByIdQuery(string id) : IRequest<Result<UserDto>>
{
    public string Id { get; set; } = id;
}
