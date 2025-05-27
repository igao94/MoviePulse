namespace Application.Movies.DTOs;

public class MovieRoleDto
{
    public string Id { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string DateOfBirth { get; set; } = string.Empty;
    public string? PictureUrl { get; set; }
    public string? CharacterName { get; set; }
    public IEnumerable<string> Roles { get; set; } = [];
}
