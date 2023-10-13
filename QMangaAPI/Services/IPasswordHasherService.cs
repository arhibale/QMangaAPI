namespace QMangaAPI.Services;

/// <summary>
/// Сераис для хэширования пароля.
/// </summary>
public interface IPasswordHasherService
{
  /// <summary>
  /// Захэшировать пароль.
  /// </summary>
  /// <param name="password">Пароль.</param>
  /// <returns>Захэшированный пароль.</returns>
  string Hash(string password);
  
  /// <summary>
  /// Сверка пароля.
  /// </summary>
  /// <param name="passwordHash">Зхэшированный пароль.</param>
  /// <param name="inputPassword">Пароль.</param>
  /// <returns>Результат сверки.</returns>
  bool Verify(string passwordHash, string inputPassword);
}