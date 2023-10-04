namespace QMangaAPI.Models;

public interface IEntity
{
  DateTime CreatedAt { get; set; }
  DateTime UpdatedIn { get; set; }
}