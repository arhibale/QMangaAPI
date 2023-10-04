namespace QMangaAPI.Models.Impl;

public class Author : IEntity
{
  public Guid Id { get; set; }

  public string Name { get; set; } = string.Empty;

  public IEnumerable<Book> Books { get; } = new List<Book>();

  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public DateTime UpdatedIn { get; set; } = DateTime.Now;
}