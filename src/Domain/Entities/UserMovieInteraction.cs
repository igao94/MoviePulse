namespace Domain.Entities;

public class UserMovieInteraction
{
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    public string MovieId { get; set; } = null!;
    public Movie Movie { get; set; } = null!;
    public int? Rating { get; set; }
    public DateTime? RatedAt { get; set; }
    public bool IsWatched { get; set; }
    public bool IsInWatchlist { get; set; }
    public DateTime? AddedToWatchlistAt { get; set; }
}
