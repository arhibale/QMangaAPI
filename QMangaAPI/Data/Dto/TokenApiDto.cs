namespace QMangaAPI.Data.Dto;

public class TokenApiDto
{
  public string AccessToken { get; init; } = string.Empty;
  public string RefreshToken { get; init; } = string.Empty;
  public string Message { get; set; } = string.Empty;
}