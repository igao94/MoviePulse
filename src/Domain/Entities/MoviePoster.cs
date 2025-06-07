namespace Domain.Entities;

public class MoviePoster
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string PublicId { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string MovieId { get; set; } = null!;
    public Movie Movie { get; set; } = null!;
}
