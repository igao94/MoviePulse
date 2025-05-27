using Application.Core;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Celebrities.Commands.AddCelebrityToMovie;

public class AddCelebrityToMovieHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AddCelebrityToMovieCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(AddCelebrityToMovieCommand request,
        CancellationToken cancellationToken)
    {
        var movie = await unitOfWork.MovieRepository.GetMovieByIdAsync(request.AddCelebrityToMovieDto.MovieId);

        if (movie is null)
        {
            return Result<Unit>.Failure("Movie not found.", 404);
        }

        var celebrity = await unitOfWork.CelebrityRepository
            .GetCelebrityByIdAsync(request.AddCelebrityToMovieDto.CelebrityId);

        if (celebrity is null)
        {
            return Result<Unit>.Failure("Celebrity not found.", 404);
        }

        var celebrityRoleTypes = await unitOfWork.CelebrityRepository
            .GetCelebrityRoleTypesByIdsAsync(request.AddCelebrityToMovieDto.RoleTypeIds);

        if (celebrityRoleTypes is null)
        {
            return Result<Unit>.Failure("Invalid role type ids.", 400);
        }

        var addResult = await AddMovieRolesAsync(movie.Id,
            celebrity.Id,
            request.AddCelebrityToMovieDto.CharacterName,
            celebrityRoleTypes);

        if (!addResult.IsSuccess)
        {
            return Result<Unit>.Failure(addResult.Error!, 400);
        }

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to add movie roles.", 400);
    }

    private async Task<Result<Unit>> AddMovieRolesAsync(string movieId,
        string celebrityId,
        string? characterName,
        IEnumerable<CelebrityRoleType> celebrityRoleTypes)
    {
        var existingMovieRoles = await unitOfWork.CelebrityRepository
            .GetMovieRolesByMovieIdAndCelebrityIdAsync(movieId, celebrityId);

        var anyCharacterNameMismatch = existingMovieRoles
            .Any(mr => !string.Equals(mr.CharacterName, characterName, StringComparison.OrdinalIgnoreCase));

        if (anyCharacterNameMismatch)
        {
            return Result<Unit>
                .Failure("Character name cannot be changed for existing roles in this movie.", 400);
        }

        var existingRoleTypeIds = existingMovieRoles.Select(mr => mr.RoleTypeId);

        var newMovieRoles = celebrityRoleTypes
            .Where(crt => !existingRoleTypeIds.Contains(crt.Id))
            .Select(crt => new MovieRole
            {
                MovieId = movieId,
                CelebrityId = celebrityId,
                RoleTypeId = crt.Id,
                CharacterName = string.IsNullOrEmpty(characterName) ? null : characterName
            });

        if (!newMovieRoles.Any())
        {
            return Result<Unit>.Failure("All selected roles already exist.", 400);
        }

        unitOfWork.CelebrityRepository.AddMovieRoles(newMovieRoles);

        return Result<Unit>.Success(Unit.Value);
    }
}
