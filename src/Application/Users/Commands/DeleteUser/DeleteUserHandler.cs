﻿using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Commands.DeleteUser;

public class DeleteUserHandler(IUnitOfWork unitOfWork,
    IUserContext userContext,
    IPhotoService photoService) : IRequestHandler<DeleteUserCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserWithPhotosAsync(userContext.GetUserId());

        if (user is null)
        {
            return Result<Unit>.Failure("User not found.", 404);
        }

        var publicIds = user.Photos.Select(u => u.PublicId);

        if (user.Photos.Any())
        {
            await photoService.DeletePhotosAsync(publicIds);
        }

        await unitOfWork.UserMovieInteractionRepository.DeleteUserInteractionsAsync(user.Id);

        await unitOfWork.RoleRepository.RemoveUserRolesAsync(user.Id);

        unitOfWork.UserRepository.RemoveUser(user);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to remove user.", 400);
    }
}
