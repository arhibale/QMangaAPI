using QMangaAPI.Models.Impl;
using QMangaAPI.Repositories.Context;

namespace QMangaAPI.Repositories.Impl;

public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
{
  public AuthorRepository(AppDbContext context) : base(context)
  {
  }
}