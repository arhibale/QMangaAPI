using System.Security.Claims;

namespace QMangaAPI.Services;

/// <summary>
/// JWT сервис.
/// </summary>
public interface IJwtService
{
  /// <summary>
  /// Создать токен на основе никнейма и роли.
  /// </summary>
  /// <param name="username">Никнейм.</param>
  /// <param name="role">Роль.</param>
  /// <returns>Токен.</returns>
  string CreateJwt(string username, string role);
  
  /// <summary>
  /// Создать обновлённый токен.
  /// </summary>
  /// <returns>Токен.</returns>
  Task<string> CreateRefreshTokenAsync();
  
  /// <summary>
  /// Взять поля из токена.
  /// </summary>
  /// <param name="token">Токен.</param>
  /// <returns>Поля из токена.</returns>
  ClaimsPrincipal GetPrincipleFromExpiredToken(string token);
}