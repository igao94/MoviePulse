using Application.Core;
using MediatR;

namespace Application.UserMovieInteractions.Commands.RemoveRating;

public class RemoveRatingCommand(string movieId) : IRequest<Result<Unit>>
{
    public string MovieId { get; set; } = movieId;
}
