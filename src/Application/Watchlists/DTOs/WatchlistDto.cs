namespace Application.Watchlists.DTOs;

public class WatchlistDto
{
    public string MovieId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int DurationInMinutes { get; set; }
    public string Rating { get; set; } = string.Empty;
    public string ReleaseDate { get; set; } = string.Empty;
    public bool IsWatched { get; set; }
    public IEnumerable<string> Celebrities { get; set; } = [];
}
