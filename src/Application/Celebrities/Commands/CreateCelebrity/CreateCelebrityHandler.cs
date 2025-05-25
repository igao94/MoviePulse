using Application.Celebrities.DTOs;
using Application.Core;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Celebrities.Commands.CreateCelebrity;

public class CreateCelebrityHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateCelebrityCommand, Result<CelebrityDto>>
{
    public async Task<Result<CelebrityDto>> Handle(CreateCelebrityCommand request,
        CancellationToken cancellationToken)
    {
        var celebrity = mapper.Map<Celebrity>(request.CreateCelebrityDto);

        unitOfWork.CelebrityRepository.AddCelebrity(celebrity);

        var celebrityRoleTypes = await unitOfWork.CelebrityRepository
            .GetCelebrityRoleTypesByIdsAsync(request.CreateCelebrityDto.RoleTypeIds);

        var celebrityRoles = celebrityRoleTypes.Select(celebrityRoleType => new CelebrityRole
        {
            CelebrityId = celebrity.Id,
            RoleTypeId = celebrityRoleType.Id
        });

        unitOfWork.CelebrityRepository.AddCelebrityToRoles(celebrityRoles);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<CelebrityDto>.Success(mapper.Map<CelebrityDto>(celebrity))
            : Result<CelebrityDto>.Failure("Failed to create celebrity.", 400);
    }
}
