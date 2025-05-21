namespace Domain.Interfaces;

public interface IRoleRepository
{
    Task<IEnumerable<string>> GetUserRoleNamesAsync(string userId);
}
