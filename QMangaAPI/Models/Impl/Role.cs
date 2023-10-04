namespace QMangaAPI.Models.Impl;

public class Role : IEntity
{
  public Guid Id { get; set; }

  public string Name { get; set; } = string.Empty;

  public IEnumerable<User> Users { get; } = new List<User>();

  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public DateTime UpdatedIn { get; set; } = DateTime.Now;
}