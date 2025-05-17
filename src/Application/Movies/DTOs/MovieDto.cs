namespace Application.Movies.DTOs;

public class MovieDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateOnly ReleaseDate { get; set; }
    public int DurationInMinutes { get; set; }
    public double Rating { get; set; }
    public string? PosterUrl { get; set; }
}
