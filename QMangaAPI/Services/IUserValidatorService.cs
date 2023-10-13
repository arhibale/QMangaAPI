namespace QMangaAPI.Services;

/// <summary>
/// Валидатор пользователя.
/// </summary>
public interface IUserValidatorService
{
  /// <summary>
  /// Проверка на уникальность никнейма.
  /// </summary>
  /// <param name="username">Никнейм.</param>
  /// <returns>Результат проверки.</returns>
  Task<bool> CheckUsernameExistAsync(string username);
  
  /// <summary>
  /// Проверка на уникальность email.
  /// </summary>
  /// <param name="email">Email.</param>
  /// <returns>Результат проверки.</returns>
  Task<bool> CheckEmailExistAsync(string email);
  
  /// <summary>
  /// Проверка на корректность email.
  /// </summary>
  /// <param name="email">Email.</param>
  /// <returns>Результат проверки.</returns>
  bool CheckEmail(string email);
  
  /// <summary>
  /// Проверка на сложность пароля.
  /// </summary>
  /// <param name="password">Пароль.</param>
  /// <param name="message">Сообщения с ошибками.</param>
  /// <returns>Результат проверки.</returns>
  bool CheckPassword(string password, out string message);
}