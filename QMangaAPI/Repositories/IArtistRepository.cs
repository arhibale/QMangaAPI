using System.Linq.Expressions;
using QMangaAPI.Models.Impl;

namespace QMangaAPI.Repositories;

/// <summary>
/// Репозиторий художников.
/// </summary>
public interface IArtistRepository
{
  /// <summary>
  /// Получить список всех художников.
  /// </summary>
  /// <returns>Список художников</returns>
  IQueryable<Artist> GetAll();
  
  /// <summary>
  /// Получить художника.
  /// </summary>
  /// <param name="expression">Условие получения.</param>
  /// <returns>Художник.</returns>
  Task<Artist?> FirstOrDefaultUserAsync(Expression<Func<Artist, bool>> expression);
}