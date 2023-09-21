namespace QMangaAPI.Models.ManyToMany;

public class ArtistBook
{
  public Guid ArtistId { get; set; }
  public Guid BookId { get; set; }
}