using System.Linq.Expressions;
using QMangaAPI.Models.Impl;

namespace QMangaAPI.Repositories;

/// <summary>
/// Репозиторий манги.
/// </summary>
public interface IBookRepository
{
  /// <summary>
  /// Создать мангу.
  /// </summary>
  /// <param name="book">Манга.</param>
  void CreateBook(Book book);
  
  /// <summary>
  /// Обновить мангу.
  /// </summary>
  /// <param name="book">Манга</param>
  void UpdateBook(Book book);
  
  /// <summary>
  /// Получить кол-во манги.
  /// </summary>
  /// <returns>Кол-во манги.</returns>
  Task<int> GetCountAsync();
  
  /// <summary>
  /// Получить страницу списока манги.
  /// </summary>
  /// <param name="page">Страница.</param>
  /// <param name="pageSize">Размер страницы.</param>
  /// <returns>Список манги.</returns>
  IQueryable<Book> GetPageAsync(int? page, int pageSize);
  
  /// <summary>
  /// Получить мангу
  /// </summary>
  /// <param name="expression">Условие получения.</param>
  /// <returns>Манга.</returns>
  Task<Book?> FirstOrDefaultBookAsync(Expression<Func<Book, bool>> expression);

  /// <summary>
  /// Получить список манги.
  /// </summary>
  /// <param name="expression">Условие получения.</param>
  /// <returns>Список манги.</returns>
  IQueryable<Book> FindBooksByCondition(Expression<Func<Book, bool>> expression);

  /// <summary>
  /// Получить мангу со всеми зависимостями.
  /// </summary>
  /// <param name="expression">Условие.</param>
  /// <returns>Манга.</returns>
  Task<Book?> FirstOrDefaultIncludeAllBookAsync(Expression<Func<Book, bool>> expression);
}