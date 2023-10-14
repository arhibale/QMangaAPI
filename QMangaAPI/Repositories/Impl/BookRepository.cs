using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using QMangaAPI.Models.Impl;
using QMangaAPI.Repositories.Context;

namespace QMangaAPI.Repositories.Impl;

public class BookRepository : RepositoryBase<Book>, IBookRepository
{
  public BookRepository(AppDbContext context) : base(context)
  {
  }

  public void CreateBook(Book book)
    => Create(book);

  public void UpdateBook(Book book)
  {
    book.UpdatedIn = DateTime.Now;
    Update(book);
  }

  public IQueryable<Book> FindBooksByCondition(Expression<Func<Book, bool>> expression)
    => FindByCondition(expression);

  public async Task<int> GetCountAsync()
    => await context.Books
      .CountAsync();


  public IQueryable<Book> GetPageAsync(int? page, int pageSize)
    => context.Books
      .Include(e => e.BookType)
      .Include(e => e.Tags)
      .Include(e => e.CoverImage)
      .Skip((page - 1 ?? 0) * pageSize)
      .Take(pageSize);

  public async Task<Book?> FirstOrDefaultBookAsync(Expression<Func<Book, bool>> expression)
    => await FirstOrDefaultAsync(expression);

  public async Task<Book?> FirstOrDefaultIncludeAllBookAsync(Expression<Func<Book, bool>> expression)
    => await context.Books
      .Include(e => e.BookType)
      .Include(e => e.Tags)
      .Include(e => e.CoverImage)
      .Include(e => e.Authors)
      .Include(e => e.Artists)
      .FirstOrDefaultAsync(expression);
}