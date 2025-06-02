using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Services;

public class MovieRatingUpdateService(IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(1);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await UpdateMovieRatingAsync();

            await Task.Delay(_checkInterval, stoppingToken);
        }
    }

    private async Task UpdateMovieRatingAsync()
    {
        using var scope = serviceScopeFactory.CreateScope();

        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var averageMovieRatings = await unitOfWork.UserMovieInteractionRepository
            .CalculateAverageMovieRatingAsync();

        foreach (var (movieId, averageMovieRating) in averageMovieRatings)
        {
            var movie = await unitOfWork.MovieRepository.GetMovieByIdAsync(movieId);

            if (movie is not null)
            {
                movie.Rating = averageMovieRating ?? 0;
            }
        }

        await unitOfWork.SaveChangesAsync();
    }
}
