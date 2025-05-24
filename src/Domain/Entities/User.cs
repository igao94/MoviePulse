namespace Domain.Entities;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; } = default!;
    public byte[] PasswordSalt { get; set; } = default!;
    public string? PictureUrl { get; set; }
    public ICollection<UserRole> Roles { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
