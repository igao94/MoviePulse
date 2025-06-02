namespace Domain.Entities;

public class Movie
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateOnly ReleaseDate { get; set; }
    public int DurationInMinutes { get; set; }
    public double Rating { get; set; }
    public DateTime LastRatingCheck { get; set; }
    public string? PosterUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<MovieRole> Celebrities { get; set; } = [];
    public ICollection<UserMovieInteraction> UserMovieInteractions { get; set; } = [];
}
