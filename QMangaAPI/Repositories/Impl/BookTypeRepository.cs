using System.Linq.Expressions;
using QMangaAPI.Models.Impl;
using QMangaAPI.Repositories.Context;

namespace QMangaAPI.Repositories.Impl;

public class BookTypeRepository : RepositoryBase<BookType>, IBookTypeRepository
{
  public BookTypeRepository(AppDbContext context) : base(context)
  {
  }
  
  public async Task<BookType?> FirstOrDefaultUserAsync(Expression<Func<BookType, bool>> expression, bool trackChanges)
    => await FirstOrDefaultAsync(expression, trackChanges);
}