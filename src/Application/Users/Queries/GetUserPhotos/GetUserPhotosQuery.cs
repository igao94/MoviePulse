using Application.Core;
using Application.Users.DTOs;
using MediatR;

namespace Application.Users.Queries.GetUserPhotos;

public class GetUserPhotosQuery : IRequest<Result<IEnumerable<PhotoDto>>>
{

}
