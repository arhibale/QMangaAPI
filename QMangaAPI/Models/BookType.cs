using QMangaAPI.Data.Interfaces.Models;

namespace QMangaAPI.Models;

public class BookType : IEntity
{
  public Guid Id { get; set; }

  public string Name { get; set; } = string.Empty;
  
  public IEnumerable<Book> Books { get; } = new List<Book>();
  
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedIn { get; set; }
}