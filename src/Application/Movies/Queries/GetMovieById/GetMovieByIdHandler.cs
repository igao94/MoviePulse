using Application.Core;
using Application.Movies.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Movies.Queries.GetMovieById;

public class GetMovieByIdHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetMovieByIdQuery, Result<MovieDto>>
{
    public async Task<Result<MovieDto>> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var movie = await unitOfWork.MovieRepository.GetMovieWithCelebritiesAndGenresByIdAsync(request.Id);

        if (movie is null)
        {
            return Result<MovieDto>.Failure("Movie not found.", 404);
        }

        var groupedCelebrities = movie.Celebrities
            .GroupBy(mr => mr.CelebrityId)
            .Select(g =>
            {
                var firstRole = g.First();

                return new MovieRoleDto
                {
                    Id = firstRole.Celebrity.Id,
                    FullName = firstRole.Celebrity.GetFullName(),
                    Bio = firstRole.Celebrity.Bio,
                    DateOfBirth = firstRole.Celebrity.DateOfBirth.ToString(),
                    PictureUrl = firstRole.Celebrity.PictureUrl,
                    CharacterName = firstRole.CharacterName,
                    Roles = g.Select(mr => mr.RoleType.Name)
                };
            })
            .ToList();

        var movieDto = mapper.Map<MovieDto>(movie);

        movieDto.Celebrities = groupedCelebrities;

        return Result<MovieDto>.Success(movieDto);
    }
}
