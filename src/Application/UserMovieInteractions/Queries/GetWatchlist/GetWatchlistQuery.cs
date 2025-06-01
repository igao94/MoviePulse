using Application.Core;
using Application.UserMovieInteractions.DTOs;
using MediatR;

namespace Application.UserMovieInteractions.Queries.GetWatchlist;

public class GetWatchlistQuery(UserMovieIntercationParams userMovieIntercationParams)
    : IRequest<Result<IEnumerable<UserMovieIntercationDto>>>
{
    public UserMovieIntercationParams UserMovieIntercationParams { get; set; } = userMovieIntercationParams;
}
