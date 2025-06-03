using Application.Core;
using Application.UserMovieInteractions.DTOs;
using MediatR;

namespace Application.UserMovieInteractions.Queries.GetUserRatings;

public class GetUserRatingsQuery(UserMovieIntercationParams userMovieIntercationParams)
    : IRequest<Result<IEnumerable<UserMovieIntercationDto>>>
{
    public UserMovieIntercationParams UserMovieIntercationParams { get; set; } = userMovieIntercationParams;
}
