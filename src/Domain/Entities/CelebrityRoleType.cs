namespace Domain.Entities;

public class CelebrityRoleType
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public ICollection<CelebrityRole> Celebrities { get; set; } = [];
}
