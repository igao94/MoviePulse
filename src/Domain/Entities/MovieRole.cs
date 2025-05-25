namespace Domain.Entities;

public class MovieRole
{
    public Celebrity Celebrity { get; set; } = null!;
    public string CelebrityId { get; set; } = null!;
    public CelebrityRoleType RoleType { get; set; } = null!;
    public string RoleTypeId { get; set; } = null!;
    public Movie Movie { get; set; } = null!;
    public string MovieId { get; set; } = null!;
    public string? CharacterName { get; set; }
}
