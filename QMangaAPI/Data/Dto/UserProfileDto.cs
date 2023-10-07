namespace QMangaAPI.Data.Dto;

public class UserProfileDto
{
  public string Username { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string Role { get; set; } = null!;
  public List<BookPageDto>? BookUploads { get; set; }
}