using QMangaAPI.Data.Interfaces.Models;

namespace QMangaAPI.Models;

public class Translator : IEntity
{
  public Guid Id { get; set; }
  
  public Guid UserId { get; set; }
  public User User { get; set; } = null!;
  
  public Guid BookId { get; set; }
  public Book Book { get; set; } = null!;
  
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedIn { get; set; }
}