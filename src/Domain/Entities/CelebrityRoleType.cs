namespace Domain.Entities;

public class CelebrityRoleType
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<MovieRole> Celebrities { get; set; } = [];
}
