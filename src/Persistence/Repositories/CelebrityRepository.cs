using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class CelebrityRepository(AppDbContext context) : ICelebrityRepository
{
    public void AddCelebrity(Celebrity celebrity) => context.Celebrities.Add(celebrity);

    public async Task<IEnumerable<CelebrityRoleType>> GetCelebrityRoleTypesByIdsAsync(IEnumerable<string> ids)
    {
        return await context.CelebrityRoleTypes
            .Where(cr => ids.Contains(cr.Id))
            .ToListAsync();
    }

    public void AddCelebrityToRoles(IEnumerable<CelebrityRole> celebrityRoles)
    {
        context.CelebrityRoles.AddRange(celebrityRoles);
    }
}
