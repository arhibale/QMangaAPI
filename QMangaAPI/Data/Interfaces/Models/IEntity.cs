namespace QMangaAPI.Data.Interfaces.Models;

/// <summary>
/// Сущность в репозитории.
/// </summary>
public interface IEntity
{
  /// <summary>
  /// Дата создания сущности.
  /// </summary>
  DateTime CreatedAt { get; set; }
  
  /// <summary>
  /// Дата последнего обновления сущности.
  /// </summary>
  DateTime UpdatedIn { get; set; }
}