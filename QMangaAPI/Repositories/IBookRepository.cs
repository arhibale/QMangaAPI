using System.Linq.Expressions;
using QMangaAPI.Models.Impl;

namespace QMangaAPI.Repositories;

public interface IBookRepository
{
  void CreateBook(Book book);
  void UpdateBook(Book book);
  Task<int> GetCountAsync();
  Task<List<Book>> GetPageAsync(int? page, int pageSize);
  Task<Book?> FirstOrDefaultBookAsync(Expression<Func<Book, bool>> expression, bool trackChanges);

  IQueryable<Book> FindBooksByCondition(Expression<Func<Book, bool>> expression, bool trackChanges);
}