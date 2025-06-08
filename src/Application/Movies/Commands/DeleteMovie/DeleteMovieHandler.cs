using Application.Core;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Movies.Commands.DeleteMovie;

public class DeleteMovieHandler(IUnitOfWork unitOfWork,
    IPhotoService photoService) : IRequestHandler<DeleteMovieComand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteMovieComand request, CancellationToken cancellationToken)
    {
        var movie = await unitOfWork.MovieRepository.GetMovieWithPostersAsync(request.Id);

        if (movie is null)
        {
            return Result<Unit>.Failure("Movie not found.", 404);
        }

        var publicIds = movie.Posters.Select(m => m.PublicId);

        if (movie.Posters.Any())
        {
            await photoService.DeletePhotosAsync(publicIds);
        }

        await unitOfWork.MovieGenreRepository.RemoveMovieGenresAsync(movie.Id);

        await unitOfWork.UserMovieInteractionRepository.DeleteMovieInteractionsAsync(movie.Id);

        await unitOfWork.MovieRepository.RemoveMovieRolesFromMovieAsync(movie.Id);

        unitOfWork.MovieRepository.RemoveMovie(movie);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to delete movie.", 400);
    }
}
