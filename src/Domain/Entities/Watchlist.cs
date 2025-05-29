namespace Domain.Entities;

public class Watchlist
{
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    public string MovieId { get; set; } = null!;
    public Movie Movie { get; set; } = null!;
    public bool IsWatched { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
