using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Command.AddPhoto;

public class AddPhotoHandler(IUnitOfWork unitOfWork,
    IPhotoService photoService,
    IUserContext userContext) : IRequestHandler<AddPhotoCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(AddPhotoCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserByIdAsync(userContext.GetUserId());

        if (user is null)
        {
            return Result<Unit>.Failure("User not found.", 404);
        }

        var uploadResult = await photoService.UploadPhotoAsync(request.File);

        if (uploadResult is null)
        {
            return Result<Unit>.Failure("Failed to upload photo to cloudinary.", 400);
        }

        AddPhoto(user, uploadResult);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to upload photo.", 400);
    }

    private void AddPhoto(User user, PhotoUploadResult uploadResult)
    {
        var photo = new UserPhoto
        {
            UserId = user.Id,
            PublicId = uploadResult.PublicId,
            Url = uploadResult.Url
        };

        unitOfWork.UserRepository.AddPhoto(photo);

        user.PictureUrl ??= photo.Url;
    }
}
