using System.Linq.Expressions;
using QMangaAPI.Models.Impl;

namespace QMangaAPI.Repositories;

/// <summary>
/// Репозиторий авторов.
/// </summary>
public interface IAuthorRepository
{
  /// <summary>
  /// Получить всех авторов.
  /// </summary>
  /// <returns></returns>
  IQueryable<Author> GetAll();
  
  /// <summary>
  /// Получить автора.
  /// </summary>
  /// <param name="expression">Условие получения</param>
  /// <returns>Автор.</returns>
  Task<Author?> FirstOrDefaultUserAsync(Expression<Func<Author, bool>> expression);
}