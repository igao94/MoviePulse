using Domain.Entities;

namespace Domain.Interfaces;

public interface ICelebrityRepository
{
    void AddCelebrity(Celebrity celebrity);
    Task<IEnumerable<CelebrityRoleType>> GetCelebrityRoleTypesByIdsAsync(IEnumerable<string> ids);
    void AddCelebrityToRoles(IEnumerable<CelebrityRole> celebrityRoles);
}
