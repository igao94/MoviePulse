using Shared.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Security;

public class HmacPasswordHasher : IHmacPasswordHasher
{
    public (byte[] Hash, byte[] Salt) HashPassword(string password)
    {
        using var hmac = new HMACSHA512();

        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return (hash, hmac.Key);
    }

    public bool VerifyPassword(byte[] hash, byte[] salt, string password)
    {
        using var hmac = new HMACSHA512(salt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return computedHash.SequenceEqual(hash);
    }
}
