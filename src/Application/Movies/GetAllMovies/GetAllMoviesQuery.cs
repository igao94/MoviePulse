using Application.Core;
using Application.Movies.DTOs;
using MediatR;

namespace Application.Movies.GetAllMovies;

public class GetAllMoviesQuery : IRequest<Result<IEnumerable<MovieDto>>>
{
}
