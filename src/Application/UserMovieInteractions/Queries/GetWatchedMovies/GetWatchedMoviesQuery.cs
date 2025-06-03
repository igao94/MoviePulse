using Application.Core;
using Application.UserMovieInteractions.DTOs;
using MediatR;

namespace Application.UserMovieInteractions.Queries.GetWatchedMovies;

public class GetWatchedMoviesQuery(UserMovieIntercationParams userMovieIntercationParams)
    : IRequest<Result<IEnumerable<UserMovieIntercationDto>>>
{
    public UserMovieIntercationParams UserMovieIntercationParams { get; set; } = userMovieIntercationParams;
}
