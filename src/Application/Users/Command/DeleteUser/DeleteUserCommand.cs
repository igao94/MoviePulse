using Application.Core;
using MediatR;

namespace Application.Users.Command.DeleteUser;

public class DeleteUserCommand : IRequest<Result<Unit>>
{

}
