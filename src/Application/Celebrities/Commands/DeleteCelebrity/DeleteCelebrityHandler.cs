using Application.Core;
using Domain.Interfaces;
using MediatR;

namespace Application.Celebrities.Commands.DeleteCelebrity;

public class DeleteCelebrityHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCelebrityCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteCelebrityCommand request, CancellationToken cancellationToken)
    {
        var celebrity = await unitOfWork.CelebrityRepository.GetCelebrityByIdAsync(request.Id);

        if (celebrity is null)
        {
            return Result<Unit>.Failure("Celebrity not found.", 404);
        }

        await unitOfWork.CelebrityRepository.RemoveMovieRolesForCelebrity(celebrity.Id);

        unitOfWork.CelebrityRepository.RemoveCelebrity(celebrity);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete celebrity.", 400);
    }
}
