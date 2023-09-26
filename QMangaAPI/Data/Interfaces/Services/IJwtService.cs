using System.Security.Claims;

namespace QMangaAPI.Data.Interfaces.Services;

public interface IJwtService
{
  string CreateJwt(string username, string role);
  Task<string> CreateRefreshTokenAsync();
  ClaimsPrincipal GetPrincipleFromExpiredToken(string token);
}