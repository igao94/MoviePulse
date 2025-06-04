using Application.Core;
using Application.Movies.DTOs;
using MediatR;

namespace Application.Movies.Queries.GetAllMovies;

public class GetAllMoviesQuery(MovieSpecParams movieSpecParams) : IRequest<Result<IEnumerable<MovieDto>>>
{
    public MovieSpecParams MovieSpecParams { get; set; } = movieSpecParams;
}
