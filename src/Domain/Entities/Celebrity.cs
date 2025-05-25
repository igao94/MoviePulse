namespace Domain.Entities;

public class Celebrity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string? PictureUrl { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<CelebrityRole> Roles { get; set; } = [];

    public string GetFullName() => $"{FirstName} {LastName}";
}
