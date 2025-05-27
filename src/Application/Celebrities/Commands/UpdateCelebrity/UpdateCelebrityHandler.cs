using Application.Core;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Celebrities.Commands.UpdateCelebrity;

public class UpdateCelebrityHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateCelebrityCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(UpdateCelebrityCommand request, CancellationToken cancellationToken)
    {
        var celebrity = await unitOfWork.CelebrityRepository
            .GetCelebrityByIdAsync(request.UpdateCelebrityDto.Id);

        if (celebrity is null)
        {
            return Result<Unit>.Failure("Celebrity not found.", 404);
        }

        mapper.Map(request.UpdateCelebrityDto, celebrity);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to update celebrity.", 400);
    }
}
