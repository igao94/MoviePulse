using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Commands.DeletePhoto;

public class DeletePhotoHandler(IUnitOfWork unitOfWork,
    IUserContext userContext,
    IPhotoService photoService) : IRequestHandler<DeletePhotoCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserWithPhotosAsync(userContext.GetUserId());

        if (user is null)
        {
            return Result<Unit>.Failure("User not found.", 400);
        }

        var photo = user.Photos.FirstOrDefault(p => p.Id == request.PhotoId);

        if (photo is null)
        {
            return Result<Unit>.Failure("Photo not found.", 404);
        }

        if (user.PictureUrl == photo.Url)
        {
            return Result<Unit>.Failure("Can't delete main photo.", 400);
        }

        await photoService.DeletePhotoAsync(photo.PublicId);

        user.Photos.Remove(photo);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete photo.", 400);
    }
}
