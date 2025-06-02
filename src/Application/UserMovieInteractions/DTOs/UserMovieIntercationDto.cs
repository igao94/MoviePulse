namespace Application.UserMovieInteractions.DTOs;

public class UserMovieIntercationDto
{
    public string MovieId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int DurationInMinutes { get; set; }
    public double Rating { get; set; }
    public string ReleaseDate { get; set; } = string.Empty;
    public bool IsWatched { get; set; }
    public IEnumerable<string> Celebrities { get; set; } = [];
}
