using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Users.Command.AddPhoto;

public class AddPhotoCommand : IRequest<Result<Unit>>
{
    public required IFormFile File { get; set; }
}
