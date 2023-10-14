namespace QMangaAPI.Models.Impl;

public class Book : IEntity
{
  public Guid Id { get; set; }

  public string Name { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;

  public Guid BookTypeId { get; set; }
  public BookType BookType { get; set; } = null!;

  public Guid UploadedByUserId { get; set; }
  public User? UploadedByUser { get; set; }

  public List<Author> Authors { get; } = new List<Author>();
  public List<Artist> Artists { get; } = new List<Artist>();
  public List<Tag> Tags { get; } = new List<Tag>();

  public CoverImage? CoverImage { get; set; }
  public IEnumerable<Image> Images { get; set; } = new List<Image>();

  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedIn { get; set; }
}