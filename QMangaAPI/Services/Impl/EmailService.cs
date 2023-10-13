using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using QMangaAPI.Data;

namespace QMangaAPI.Services.Impl;

public class EmailService : IEmailService
{
  private readonly EmailSettings emailSettings;

  public EmailService(IOptions<EmailSettings> options)
  {
    this.emailSettings = options.Value;
  }

  public async Task SendEmailAsync(EmailRequest request)
  {
    var email = new MimeMessage();
    email.Sender = MailboxAddress.Parse(emailSettings.Email);
    email.To.Add(MailboxAddress.Parse(request.ToEmail.Single()));
    email.Subject = request.Subject;

    var builder = new BodyBuilder { HtmlBody = request.Body };
    email.Body = builder.ToMessageBody();

    await Send(email);
  }

  public async Task SendRangeEmailAsync(EmailRequest request)
  {
    var email = new MimeMessage();
    email.Sender = MailboxAddress.Parse(emailSettings.Email);
    email.To.AddRange(request.ToEmail.Select(MailboxAddress.Parse));
    email.Subject = request.Subject;

    var builder = new BodyBuilder { HtmlBody = request.Body };
    email.Body = builder.ToMessageBody();

    await Send(email);
  }

  private async Task Send(MimeMessage email)
  {
    using var smtp = new SmtpClient();
    await smtp.ConnectAsync(emailSettings.Host, emailSettings.Port, SecureSocketOptions.StartTls);
    await smtp.AuthenticateAsync(emailSettings.Email, emailSettings.Password);
    await smtp.SendAsync(email);
    await smtp.DisconnectAsync(true);
  }
}