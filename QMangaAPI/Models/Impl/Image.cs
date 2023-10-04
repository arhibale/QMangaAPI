using System.ComponentModel.DataAnnotations.Schema;

namespace QMangaAPI.Models.Impl;

public class Image : IEntity
{
  public Guid Id { get; set; }

  public string Name { get; set; } = string.Empty;
  public string Path { get; set; } = string.Empty;

  public Guid BookId { get; set; }
  public Book Book { get; set; } = null!;

  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public DateTime UpdatedIn { get; set; } = DateTime.Now;

  [NotMapped] public IFormFile? File { get; set; }
}