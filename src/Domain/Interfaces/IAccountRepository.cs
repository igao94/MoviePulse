using Domain.Entities;

namespace Domain.Interfaces;

public interface IAccountRepository
{
    void RegisterUser(User user);
}
