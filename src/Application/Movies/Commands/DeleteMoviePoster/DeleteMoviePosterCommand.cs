using Application.Core;
using MediatR;

namespace Application.Movies.Commands.DeleteMoviePoster;

public class DeleteMoviePosterCommand(string movieId, string posterId) : IRequest<Result<Unit>>
{
    public string MovieId { get; set; } = movieId;
    public string PosterId { get; set; } = posterId;
}
