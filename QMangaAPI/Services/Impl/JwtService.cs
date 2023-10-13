using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using QMangaAPI.Repositories;

namespace QMangaAPI.Services.Impl;

public class JwtService : IJwtService
{
  private readonly IRepositoryManager repositoryManager;

  public JwtService(IRepositoryManager repositoryManager)
  {
    this.repositoryManager = repositoryManager;
  }

  public string CreateJwt(string username, string role)
  {
    var jwtTokenHandler = new JwtSecurityTokenHandler();
    var key = "qmangaveryverysecret"u8.ToArray();
    var identity = new ClaimsIdentity(new Claim[]
    {
      new(ClaimTypes.Role, role),
      new(ClaimTypes.Name, username),
    });

    var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = identity,
      Expires = DateTime.Now.AddMinutes(5),
      SigningCredentials = credentials
    };

    var token = jwtTokenHandler.CreateToken(tokenDescriptor);
    return jwtTokenHandler.WriteToken(token);
  }

  public async Task<string> CreateRefreshTokenAsync()
  {
    while (true)
    {
      var tokenBytes = RandomNumberGenerator.GetBytes(64);
      var refreshToken = Convert.ToBase64String(tokenBytes);

      if (await repositoryManager.Users.AnyUserAsync(e => e.RefreshToken == refreshToken))
        continue;

      return refreshToken;
    }
  }

  public ClaimsPrincipal GetPrincipleFromExpiredToken(string token)
  {
    var key = "qmangaveryverysecret"u8.ToArray();
    var tokenValidationParameters = new TokenValidationParameters
    {
      ValidateAudience = false,
      ValidateIssuer = false,
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(key),
      ValidateLifetime = false
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
    if (securityToken is not JwtSecurityToken jwtSecurityToken ||
        !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.OrdinalIgnoreCase))
    {
      throw new SecurityTokenException("This is invalid token");
    }

    return principal;
  }
}