using Domain.Entities;

namespace Domain.Interfaces;

public interface ICelebrityRepository
{
    void AddCelebrity(Celebrity celebrity);
    Task<IEnumerable<CelebrityRoleType>?> GetCelebrityRoleTypesByIdsAsync(IEnumerable<string> ids);
    Task<Celebrity?> GetCelebrityByIdAsync(string id);
    void AddMovieRoles(IEnumerable<MovieRole> movieRoles);
    Task<IEnumerable<MovieRole>> GetMovieRolesByMovieIdAndCelebrityIdAsync(string movieId, string celebrityId);
    Task<(IEnumerable<Celebrity>, DateTime?)> GetAllCelebritiesAsync(string? search,
        int pageSize,
        DateTime? cursor);
    void RemoveCelebrity(Celebrity celebrity);
    Task RemoveMovieRolesForCelebrity(string id);
}
