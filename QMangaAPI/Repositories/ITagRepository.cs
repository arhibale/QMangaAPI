using System.Linq.Expressions;
using QMangaAPI.Models.Impl;

namespace QMangaAPI.Repositories;

/// <summary>
/// Репозиторий тегов.
/// </summary>
public interface ITagRepository
{
  /// <summary>
  /// Получить все теги.
  /// </summary>
  /// <returns>Тег</returns>
  IQueryable<Tag> GetAll();
  
  /// <summary>
  /// Получить тег.
  /// </summary>
  /// <param name="expression">Условие получения.</param>
  /// <returns>Тег.</returns>
  Task<Tag?> FirstOrDefaultUserAsync(Expression<Func<Tag, bool>> expression);
}