using System.ComponentModel.DataAnnotations;
using QMangaAPI.Data.Interfaces.Models;

namespace QMangaAPI.Models;

public class Book : IEntity
{
  [Key] public Guid Id { get; set; }

  public string Name { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;

  public Guid BookTypeId { get; set; }
  public BookType BookType { get; set; } = null!;
  
  public Guid UserId { get; set; }
  public User User { get; set; } = null!;
  
  public IEnumerable<Author> Authors { get; } = new List<Author>();
  public IEnumerable<Artist> Artists { get; } = new List<Artist>();
  public IEnumerable<Tag> Tags { get; } = new List<Tag>();
  public IEnumerable<Comment> Comments { get; } = new List<Comment>();
  public IEnumerable<Translator> Translators { get; } = new List<Translator>();

  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedIn { get; set; }
}