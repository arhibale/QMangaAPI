using System.ComponentModel.DataAnnotations;
using QMangaAPI.Data.Interfaces.Models;

namespace QMangaAPI.Models;

public class Comment : IEntity
{
  [Key] public Guid Id { get; set; }

  public string Text { get; set; } = string.Empty;
  
  public Guid BookId { get; set; }
  public Book Book { get; set; } = null!;
  
  public Guid UserId { get; set; }
  public User User { get; set; } = null!;

  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedIn { get; set; }
}