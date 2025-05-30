namespace Domain.Entities;

public class UserMovieRating
{
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    public string MovieId { get; set; } = null!;
    public Movie Movie { get; set; } = null!;
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
