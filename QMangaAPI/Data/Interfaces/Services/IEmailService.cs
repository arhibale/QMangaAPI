using QMangaAPI.Helpers;

namespace QMangaAPI.Data.Interfaces.Services;

public interface IEmailService
{
  Task SendEmailAsync(EmailRequest request);
  Task SendRangeEmailAsync(EmailRequest request);
}