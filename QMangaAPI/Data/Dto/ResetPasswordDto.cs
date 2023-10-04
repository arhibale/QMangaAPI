namespace QMangaAPI.Data.Dto;

public class ResetPasswordDto
{
  public string Email { get; set; } = string.Empty;
  public string EmailToken { get; set; } = string.Empty;
  public string NewPassword { get; set; } = string.Empty;
}