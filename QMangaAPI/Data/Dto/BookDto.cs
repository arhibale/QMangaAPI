namespace QMangaAPI.Data.Dto;

public class BookDto
{
  public Guid Id { get; set; }

  public string Name { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public string BookType { get; set; } = string.Empty!;

  public List<string> Authors { get; set; } = new();
  public List<string> Artists { get; set; }= new();
  public List<string> Tags { get; set; } = new();

  public IFormFile? CoverImage { get; set; }
  public List<IFormFile> Images { get; set; } = new();
}