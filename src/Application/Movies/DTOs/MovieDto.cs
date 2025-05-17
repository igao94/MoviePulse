namespace Application.Movies.DTOs;

public class MovieDto : BaseMovieDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? PosterUrl { get; set; }
}
