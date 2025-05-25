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
        var movie = await unitOfWork.MovieRepository.GetMovieByIdAsync(request.MovieId);

        if (movie is null)
        {
            return Result<Unit>.Failure("Movie not found.", 404);
        }

        var celebrity = await unitOfWork.CelebrityRepository.GetCelebrityByIdAsync(request.CelebrityId);

        if (celebrity is null)
        {
            return Result<Unit>.Failure("Celebrity not found.", 404);
        }

        var celebrityRoleTypes = await unitOfWork.CelebrityRepository
            .GetCelebrityRoleTypesByIdsAsync(request.RoleTypeIds);

        if (celebrityRoleTypes is null)
        {
            return Result<Unit>.Failure("Invalid role type ids.", 400);
        }

        var movieRoles = await AddMovieRolesAsync(movie.Id, celebrity.Id, celebrityRoleTypes);

        if (movieRoles is null)
        {
            return Result<Unit>
                .Failure("All selected roles already exist for this celebrity in the movie.", 400);
        }

        unitOfWork.CelebrityRepository.AddMovieRoles(movieRoles);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to add movie roles.", 400);
    }

    private async Task<IEnumerable<MovieRole>?> AddMovieRolesAsync(string movieId,
        string celebrityId,
        IEnumerable<CelebrityRoleType> celebrityRoleTypes)
    {
        var existingMovieRoles = await unitOfWork.CelebrityRepository
            .GetMovieRolesByMovieIdAndCelebrityIdAsync(movieId, celebrityId);

        var existingRoleTypeIds = existingMovieRoles.Select(mr => mr.RoleTypeId);

        var newMovieRoles = celebrityRoleTypes
            .Where(celebrityRoleType => !existingRoleTypeIds.Contains(celebrityRoleType.Id))
            .Select(celebrityRoleType => new MovieRole
            {
                MovieId = movieId,
                CelebrityId = celebrityId,
                RoleTypeId = celebrityRoleType.Id
            });

        if (!newMovieRoles.Any())
        {
            return null;
        }

        return newMovieRoles;
    }
}
