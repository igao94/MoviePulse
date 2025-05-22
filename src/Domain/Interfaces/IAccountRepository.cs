using Domain.Entities;

namespace Domain.Interfaces;

public interface IAccountRepository
{
    Task<User?> GetUserByIdAsync(string id);
    void AddUser(User user);
    Task<User?> GetUserByEmailAsync(string email);
    Task<bool> IsUsernameTakenAsync(string username);
    Task<bool> IsEmailTakenAsync(string email);
}
