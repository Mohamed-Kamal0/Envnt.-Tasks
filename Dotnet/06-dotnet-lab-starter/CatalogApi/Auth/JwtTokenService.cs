using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;

namespace CatalogApi.Auth;

public class JwtTokenService : ITokenService
{
    private readonly IConfiguration _config;
    public JwtTokenService(IConfiguration config) => _config = config;

    public string CreateToken(string username, string role)
    {
        // TODO (you) — Day 6: read the "Jwt" config section, build a SymmetricSecurityKey + SigningCredentials,
        // add Name and Role claims (the Role claim is what [Authorize(Roles = "Admin")] reads later),
        // create a JwtSecurityToken (issuer, audience, claims, expires) and return WriteToken(token).
        //throw new NotImplementedException();
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier,username),
            new Claim(ClaimTypes.Role,role)
        };
        var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddHours(2), issuer: _config.GetSection("jwt")["Issuer"],
        audience: _config.GetSection("jwt")["Audience"], signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("jwt")["Key"]!)), Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature));
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
