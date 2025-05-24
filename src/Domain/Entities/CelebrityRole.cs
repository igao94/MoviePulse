namespace Domain.Entities;

public class CelebrityRole
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Celebrity Celebrity { get; set; } = null!;
    public string CelebrityId { get; set; } = null!;
    public CelebrityRoleType RoleType { get; set; } = null!;
    public string RoleTypeId { get; set; } = null!;
}
