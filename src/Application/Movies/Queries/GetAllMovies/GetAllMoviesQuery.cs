using Application.Core;
using Application.Movies.DTOs;
using MediatR;

namespace Application.Movies.Queries.GetAllMovies;

public class GetAllMoviesQuery(MovieSpecParams movieSpecParams) 
    : IRequest<Result<PagedList<MovieDto, DateTime?>>>
{
    public MovieSpecParams MovieSpecParams { get; set; } = movieSpecParams;
}
