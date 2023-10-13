namespace QMangaAPI.Data;

/// <summary>
/// Тело html документа для сброса пароля.
/// </summary>
public static class EmailPasswordResetBody
{
  public static string EmailStringBody(string email, string emailToken)
  {
    return $"""
            <html>
            <head>
            </head>
            <body style="margin:0; padding:0; font-family:Arial, Helvetica, sans-serif;">
              <div style="height:auto; width:400px; padding: 30px>">
                <div>
                  <h1>Reset your Password</h1>
                  <hr>
                  <p>You're receiving this e-mail because you requested a password reset for your QManga account.</p>
                  <p>Please tap the button below to choose a new password.</p>
                  <a href="http://localhost:4200/reset?email={email}&code={emailToken}" target="_blank" style="background:#0d6efd;padding:10px;border:none;
                    color:white; border-radius:4px;display:block;margin:0 auto;width:50%;text-align:center:text-decoration:none">Reset Password</a><br>
                  <p>Kind Regards, <br><br>QManga</p>
                </div>
              </div>
            </body>
            </html>
            """;
  }
}