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

    public async Task<Celebrity?> GetCelebrityByIdAsync(string id) => await context.Celebrities.FindAsync(id);

    public void AddMovieRoles(IEnumerable<MovieRole> movieRoles) => context.MovieRoles.AddRange(movieRoles);

    public async Task<IEnumerable<MovieRole>> GetMovieRolesByMovieIdAndCelebrityIdAsync(string movieId,
        string celebrityId)
    {
        return await context.MovieRoles
            .Where(mr => mr.MovieId == movieId && mr.CelebrityId == celebrityId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Celebrity>> GetAllCelebritiesAsync(string? search, string? sort)
    {
        var query = context.Celebrities
            .OrderBy(c => c.CreatedAt)
            .AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(c => c.FirstName.Contains(search) || c.LastName.Contains(search));
        }

        query = sort switch
        {
            "nameAsc" => query.OrderBy(c => c.FirstName),
            "nameDesc" => query.OrderByDescending(c => c.LastName),
            "dateOfBirthAsc" => query.OrderBy(c => c.DateOfBirth),
            "dateOfBirthDesc" => query.OrderByDescending(c => c.DateOfBirth),
            _ => query
        };

        return await query.ToListAsync();
    }

    public void RemoveCelebrity(Celebrity celebrity) => context.Celebrities.Remove(celebrity);

    public async Task RemoveMovieRolesForCelebrity(string id)
    {
        var movieRoles = await context.MovieRoles
            .Where(mr => mr.CelebrityId == id)
            .ToListAsync();

        context.MovieRoles.RemoveRange(movieRoles);
    }
}
