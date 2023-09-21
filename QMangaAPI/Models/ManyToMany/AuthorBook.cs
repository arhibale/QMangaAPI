namespace QMangaAPI.Models.ManyToMany;

public class AuthorBook
{
  public Guid AuthorId { get; set; }
  public Guid BookId { get; set; }
}