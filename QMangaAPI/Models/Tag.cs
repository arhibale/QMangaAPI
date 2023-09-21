using System.ComponentModel.DataAnnotations;
using QMangaAPI.Data.Interfaces.Models;

namespace QMangaAPI.Models;

public class Tag : IEntity
{
  [Key] public Guid Id { get; set; }

  public string Name { get; set; } = string.Empty;
  
  public IEnumerable<Book> Books = new List<Book>();

  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedIn { get; set; }
}