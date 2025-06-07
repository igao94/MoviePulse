using Application.Core;
using MediatR;

namespace Application.Users.Command.DeletePhoto;

public class DeletePhotoCommand(string photoId) : IRequest<Result<Unit>>
{
    public string PhotoId { get; set; } = photoId;
}
