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
}
