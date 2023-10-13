namespace QMangaAPI.Models;

/// <summary>
/// Сущность в репозитории.
/// </summary>
public interface IEntity
{
  DateTime CreatedAt { get; set; }
  DateTime UpdatedIn { get; set; }
}