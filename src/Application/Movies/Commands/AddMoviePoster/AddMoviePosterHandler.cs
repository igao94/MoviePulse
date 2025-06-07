using Application.Core;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Movies.Commands.AddMoviePoster;

public class AddMoviePosterHandler(IUnitOfWork unitOfWork,
    IPhotoService photoService) : IRequestHandler<AddMoviePosterCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(AddMoviePosterCommand request, CancellationToken cancellationToken)
    {
        var movie = await unitOfWork.MovieRepository.GetMovieByIdAsync(request.Id);

        if (movie is null)
        {
            return Result<Unit>.Failure("Movie not found.", 400);
        }

        var uploadResult = await photoService.UploadPhotoAsync(request.File);

        if (uploadResult is null)
        {
            return Result<Unit>.Failure("Failed to upload photo to cloudinary.", 400);
        }

        AddPoster(movie, uploadResult);

        var result = await unitOfWork.SaveChangesAsync();

        return result
            ? Result<Unit>.Success(Unit.Value)
            : Result<Unit>.Failure("Failed to add poster.", 400);
    }

    private static void AddPoster(Movie movie, PhotoUploadResult uploadResult)
    {
        var moviePoster = new MoviePoster
        {
            MovieId = movie.Id,
            PublicId = uploadResult.PublicId,
            Url = uploadResult.Url
        };

        movie.Posters.Add(moviePoster);

        movie.PosterUrl ??= moviePoster.Url;
    }
}
