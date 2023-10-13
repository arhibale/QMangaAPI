using System.Linq.Expressions;
using QMangaAPI.Models.Impl;

namespace QMangaAPI.Repositories;

/// <summary>
/// Репозиторий типа манги.
/// </summary>
public interface IBookTypeRepository
{
  /// <summary>
  /// Получить тип манги.
  /// </summary>
  /// <param name="expression">Условие получения.</param>
  /// <returns>Тип манги.</returns>
  Task<BookType?> FirstOrDefaultBookTypeAsync(Expression<Func<BookType, bool>> expression);
}