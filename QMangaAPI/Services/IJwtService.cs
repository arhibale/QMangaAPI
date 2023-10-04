using System.Security.Claims;

namespace QMangaAPI.Services;

public interface IJwtService
{
  string CreateJwt(string username, string role);
  Task<string> CreateRefreshTokenAsync();
  ClaimsPrincipal GetPrincipleFromExpiredToken(string token);
}