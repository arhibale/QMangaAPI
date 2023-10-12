namespace QMangaAPI.Data.Dto;

public class ImageDto
{
  public IFormFile File { get; set; } = null!;
  public string Url { get; set; } = string.Empty;
}