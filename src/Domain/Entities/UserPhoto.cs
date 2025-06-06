namespace Domain.Entities;

public class UserPhoto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string PublicId { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
}
