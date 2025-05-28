using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(string id);
    void AddUser(User user);
    void RemoveUser(User user);
    Task<User?> GetUserByEmailAsync(string email);
    Task<bool> IsUsernameTakenAsync(string username);
    Task<bool> IsEmailTakenAsync(string email);
    Task RemoveUserWatchlistAsync(string id);
}
