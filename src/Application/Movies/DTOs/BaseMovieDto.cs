namespace Application.Movies.DTOs;

public class BaseMovieDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ReleaseDate { get; set; } = string.Empty;
    public int DurationInMinutes { get; set; }
}
