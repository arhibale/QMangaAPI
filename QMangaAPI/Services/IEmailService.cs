using QMangaAPI.Data;

namespace QMangaAPI.Services;

public interface IEmailService
{
  Task SendEmailAsync(EmailRequest request);
  Task SendRangeEmailAsync(EmailRequest request);
}