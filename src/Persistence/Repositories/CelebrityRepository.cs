using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class CelebrityRepository(AppDbContext context) : ICelebrityRepository
{
    public void AddCelebrity(Celebrity celebrity) => context.Celebrities.Add(celebrity);

    public async Task<IEnumerable<CelebrityRoleType>?> GetCelebrityRoleTypesByIdsAsync(IEnumerable<string> ids)
    {
        var celebrityRoleTypes = await context.CelebrityRoleTypes
            .Where(cr => ids.Contains(cr.Id))
            .ToListAsync();

        if (celebrityRoleTypes.Count != ids.Count())
        {
            return null;
        }

        return celebrityRoleTypes;
    }

    public void AddCelebrityToRoles(IEnumerable<CelebrityRole> celebrityRoles)
    {
        context.CelebrityRoles.AddRange(celebrityRoles);
    }
}
