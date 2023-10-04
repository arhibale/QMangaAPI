using QMangaAPI.Models.Impl;
using QMangaAPI.Repositories.Context;

namespace QMangaAPI.Repositories.Impl;

public class BookTypeRepository : RepositoryBase<BookType>, IBookTypeRepository
{
  public BookTypeRepository(AppDbContext context) : base(context)
  {
  }
}