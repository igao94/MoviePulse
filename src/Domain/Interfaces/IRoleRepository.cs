namespace Domain.Interfaces;

public interface IRoleRepository
{
    Task<IEnumerable<string>> GetUserRoleNamesAsync(string userId);
    void AddUserToRole(string userId, string roleId);
    Task RemoveUserRolesAsync(string userId);
}
