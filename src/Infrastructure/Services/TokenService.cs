using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services;

public class TokenService(IConfiguration config, IUnitOfWork unitOfWork) : ITokenService
{
    public async Task<string> GetTokenAsync(User user)
    {
        var creds = GetCredentials();

        var claims = GetClaims(user);

        var roleNames = await unitOfWork.RoleRepository.GetUserRoleNamesAsync(user.Id);

        claims.AddRange(roleNames.Select(r => new Claim(ClaimTypes.Role, r)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = config["Jwt:Issuer"],
            Audience = config["Jwt:Audience"],
            SigningCredentials = creds,
            Expires = DateTime.UtcNow.AddDays(1)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private static List<Claim> GetClaims(User user)
    {
        List<Claim> claims =
        [
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Email, user.Email),
            new("FirstName", user.FirstName),
            new("LastName", user.LastName)
        ];

        return claims;
    }

    private SigningCredentials GetCredentials()
    {
        var tokenKey = config["Jwt:TokenKey"] ?? throw new Exception("Can't access token key.");

        if (tokenKey.Length < 64)
        {
            throw new Exception("TokenKey needs to be longer.");
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        return new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
    }
}
