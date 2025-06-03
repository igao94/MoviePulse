using Application.Core;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Movies.Commands.AddGenreToMovie;

public class AddGenreToMovieHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AddGenreToMovieCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(AddGenreToMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await unitOfWork.MovieRepository.GetMovieByIdAsync(request.AddGenreToMovieDto.MovieId);

        if (movie is null)
        {
            return Result<Unit>.Failure("Movie not found.", 404);
        }

        var genre = await unitOfWork.MovieGenreRepository
            .GetGenreByIdAsync(request.AddGenreToMovieDto.GenreId);

        if (genre is null)
        {
            return Result<Unit>.Failure("Genre not found.", 404);
        }

        var movieGenre = await unitOfWork.MovieGenreRepository.GetMovieGenreByIdAsync(movie.Id, genre.Id);

        if (movieGenre is null)
        {
            movieGenre = new MovieGenre
            {
                MovieId = movie.Id,
                GenreId = genre.Id,
            };

            unitOfWork.MovieGenreRepository.AddMovieGenre(movieGenre);
        }
        else
        {
            return Result<Unit>.Failure("The movie already has this genre.", 400);
        }

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to add genre to movie.", 400);
    }
}
