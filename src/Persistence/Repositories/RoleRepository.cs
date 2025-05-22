using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class RoleRepository(AppDbContext context) : IRoleRepository
{
    public async Task<IEnumerable<string>> GetUserRoleNamesAsync(string userId)
    {
        return await context.UserRoles
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.Role.Name)
            .ToListAsync();
    }

    public void AddUserToRole(string userId, string roleId)
    {
        context.UserRoles.Add(new UserRole { UserId = userId, RoleId = roleId });
    }
}
