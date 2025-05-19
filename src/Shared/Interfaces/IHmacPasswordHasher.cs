namespace Shared.Interfaces;

public interface IHmacPasswordHasher
{
    (byte[] Hash, byte[] Salt) HashPassword(string password);
}
