namespace Application.Celebrities.DTOs;

public class AddCelebrityToMovieDto
{
    public string MovieId { get; set; } = string.Empty;
    public string CelebrityId { get; set; } = string.Empty;
    public string? CharacterName { get; set; }
    public IEnumerable<string> RoleTypeIds { get; set; } = [];
}
