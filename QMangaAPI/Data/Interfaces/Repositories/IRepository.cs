using QMangaAPI.Data.Interfaces.Models;
using QMangaAPI.Models;

namespace QMangaAPI.Data.Interfaces.Repositories;

/// <summary>
/// Репозиторий сущностей.
/// </summary>
/// <typeparam name="T">Сущность в репозитории.</typeparam>
/// <typeparam name="TK">Id сущности в репозитории.</typeparam>
public interface IRepository<T, in TK> where T : IEntity
{
  /// <summary>
  /// Взять все сущности из репозитории.
  /// </summary>
  /// <returns>Список сущностей.</returns>
  Task<IEnumerable<T>> GetAllAsync();
  
  /// <summary>
  /// Взять сущности из репозитория по id.
  /// </summary>
  /// <param name="id">Id сущности.</param>
  /// <returns></returns>
  Task<T?> GetByIdAsync(TK id);
  
  /// <summary>
  /// Добавить сущность в репоозиторий.
  /// </summary>
  /// <param name="entity">Сущность.</param>
  /// <returns>Результат добавления.</returns>
  Task<bool> AddAsync(T entity);
  
  /// <summary>
  /// Обновить сущность в репозитории.
  /// </summary>
  /// <param name="entity">Сущность.</param>
  /// <returns>Результат обновления.</returns>
  Task<bool> UpdateAsync(T entity);
  
  /// <summary>
  /// Удалить сущность из репозитория.
  /// </summary>
  /// <param name="entity">Сущность.</param>
  /// <returns>Результат удаления.</returns>
  Task<bool> DeleteAsync(T entity);
  
  /// <summary>
  /// Сохранить все изменения в репозитоории.
  /// </summary>
  /// <returns>Результат сохранения.</returns>
  Task<bool> SaveAllAsync();
}