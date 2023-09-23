using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using QMangaAPI.Data.Interfaces.Services;

namespace QMangaAPI.Services;

public class JwtGenerator : IJwtGenerator
{
  public string CreateJwt(string username, string role)
  {
    var jwtTokenHandler = new JwtSecurityTokenHandler();
    var key = "qmangaveryverysecret"u8.ToArray();
    var identity = new ClaimsIdentity(new Claim[]
    {
      new (ClaimTypes.Role, role),
      new (ClaimTypes.Name, username),
    });

    var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = identity,
      Expires = DateTime.Now.AddDays(32),
      SigningCredentials = credentials
    };

    var token = jwtTokenHandler.CreateToken(tokenDescriptor);
    return jwtTokenHandler.WriteToken(token);
  }
}