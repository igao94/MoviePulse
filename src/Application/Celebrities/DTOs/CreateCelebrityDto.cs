namespace Application.Celebrities.DTOs;

public class CreateCelebrityDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string DateOfBirth { get; set; } = string.Empty;
    public IEnumerable<string> RoleTypeIds { get; set; } = [];
}
