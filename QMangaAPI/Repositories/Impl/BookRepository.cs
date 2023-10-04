using Microsoft.EntityFrameworkCore;
using QMangaAPI.Models.Impl;
using QMangaAPI.Repositories.Context;

namespace QMangaAPI.Repositories.Impl;

public class BookRepository : RepositoryBase<Book>, IBookRepository
{
  public BookRepository(AppDbContext context) : base(context)
  {
  }

  public async Task<int> GetCountAsync()
    => await context.Books
      .AsNoTracking()
      .CountAsync();


  public async Task<List<Book>> GetPageAsync(int? page, int pageSize)
    => await context.Books
      .AsNoTracking()
      .Include(e => e.BookType)
      .Include(e => e.Tags)
      .Skip((page - 1 ?? 0) * pageSize)
      .Take(pageSize)
      .ToListAsync();
}