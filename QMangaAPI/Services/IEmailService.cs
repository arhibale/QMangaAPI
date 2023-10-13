using QMangaAPI.Data;

namespace QMangaAPI.Services;

/// <summary>
/// Сервис для отправки email письма.
/// </summary>
public interface IEmailService
{
  /// <summary>
  /// Отправить письмо.
  /// </summary>
  /// <param name="request">Модель письма.</param>
  /// <returns></returns>
  Task SendEmailAsync(EmailRequest request);
  
  /// <summary>
  /// Отправить письмо на несколько адресов.
  /// </summary>
  /// <param name="request">Модель письма.</param>
  /// <returns></returns>
  Task SendRangeEmailAsync(EmailRequest request);
}