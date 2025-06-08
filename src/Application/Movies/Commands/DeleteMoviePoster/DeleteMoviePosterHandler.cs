using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Movies.Commands.DeleteMoviePoster;

public class DeleteMoviePosterHandler(IUnitOfWork unitOfWork,
    IPhotoService photoService) : IRequestHandler<DeleteMoviePosterCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteMoviePosterCommand request,
        CancellationToken cancellationToken)
    {
        var movie = await unitOfWork.MovieRepository.GetMovieWithPostersAsync(request.MovieId);

        if (movie is null)
        {
            return Result<Unit>.Failure("Movie not found.", 404);
        }

        var poster = movie.Posters.FirstOrDefault(p => p.Id == request.PosterId);

        if (poster is null)
        {
            return Result<Unit>.Failure("Poster not found.", 404);
        }

        if (movie.PosterUrl == poster.Url)
        {
            return Result<Unit>.Failure("You can't delete main poster.", 400);
        }

        await photoService.DeletePhotoAsync(poster.PublicId);

        movie.Posters.Remove(poster);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete movie poster.", 400);
    }
}
