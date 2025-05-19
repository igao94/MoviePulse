using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Persistence.Repositories;

public class AccountRepository(AppDbContext context) : IAccountRepository
{
    public void RegisterUser(User user) => context.Users.Add(user);
}
