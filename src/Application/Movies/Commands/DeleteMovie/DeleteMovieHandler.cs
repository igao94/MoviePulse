using Application.Core;
using Domain.Interfaces;
using MediatR;

namespace Application.Movies.Commands.DeleteMovie;

public class DeleteMovieHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteMovieComand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteMovieComand request, CancellationToken cancellationToken)
    {
        var movie = await unitOfWork.MovieRepository.GetMovieByIdAsync(request.Id);

        if (movie is null)
        {
            return Result<Unit>.Failure("Movie not found.", 404);
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
