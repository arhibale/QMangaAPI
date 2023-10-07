namespace QMangaAPI.Data.Dto;

public class BookPageDto
{
  public string Name { get; set; } = string.Empty;
  public string BookType { get; set; } = string.Empty;
  public IEnumerable<string>? Tags { get; set; }
}