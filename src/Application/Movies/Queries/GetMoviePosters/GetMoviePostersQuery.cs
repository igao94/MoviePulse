using Application.Core;
using Application.Movies.DTOs;
using MediatR;

namespace Application.Movies.Queries.GetMoviePosters;

public class GetMoviePostersQuery(string id) : IRequest<Result<IEnumerable<PosterDto>>>
{
    public string Id { get; set; } = id;
}
