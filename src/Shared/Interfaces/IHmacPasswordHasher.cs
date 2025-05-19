namespace Shared.Interfaces;

public interface IHmacPasswordHasher
{
    (byte[] Hash, byte[] Salt) HashPassword(string password);
    bool VerifyPassword(byte[] hash, byte[] salt, string password);
}
