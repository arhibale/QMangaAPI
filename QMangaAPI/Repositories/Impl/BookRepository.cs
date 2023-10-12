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

  public IQueryable<Book> FindBooksByCondition(Expression<Func<Book, bool>> expression, bool trackChanges)
    => FindByCondition(expression, trackChanges);

  public async Task<int> GetCountAsync()
    => await context.Books
      .AsNoTracking()
      .CountAsync();


  public async Task<List<Book>> GetPageAsync(int? page, int pageSize)
    => await context.Books
      .AsNoTracking()
      .Include(e => e.BookType)
      .Include(e => e.Tags)
      .Include(e => e.CoverImage)
      .Skip((page - 1 ?? 0) * pageSize)
      .Take(pageSize)
      .ToListAsync();

  public async Task<Book?> FirstOrDefaultBookAsync(Expression<Func<Book, bool>> expression, bool trackChanges)
    => await FirstOrDefaultAsync(expression, trackChanges);
}