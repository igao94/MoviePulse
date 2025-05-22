using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories;

public class AccountRepository(AppDbContext context) : IAccountRepository
{
    public async Task<User?> GetUserByIdAsync(string id) => await context.Users.FindAsync(id);

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> IsEmailTakenAsync(string email)
    {
        return await context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> IsUsernameTakenAsync(string username)
    {
        return await context.Users.AnyAsync(u => u.Username == username);
    }

    public void AddUser(User user) => context.Users.Add(user);
}
