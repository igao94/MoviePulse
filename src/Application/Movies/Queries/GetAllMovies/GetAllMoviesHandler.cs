using Application.Core;
using Application.Movies.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Movies.Queries.GetAllMovies;

public class GetAllMoviesHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAllMoviesQuery, Result<IEnumerable<MovieDto>>>
{
    public async Task<Result<IEnumerable<MovieDto>>> Handle(GetAllMoviesQuery request,
        CancellationToken cancellationToken)
    {
        var movies = await unitOfWork.MovieRepository.GetAllMoviesAsync();

        return Result<IEnumerable<MovieDto>>.Success(mapper.Map<IEnumerable<MovieDto>>(movies));
    }
}
