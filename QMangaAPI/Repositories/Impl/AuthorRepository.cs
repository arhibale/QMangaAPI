using System.Linq.Expressions;
using QMangaAPI.Models.Impl;
using QMangaAPI.Repositories.Context;

namespace QMangaAPI.Repositories.Impl;

public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
{
  public AuthorRepository(AppDbContext context) : base(context)
  {
  }
  
  public IQueryable<Author> GetAll(bool trackChanges) =>
    FindAll(trackChanges);
  
  public async Task<Author?> FirstOrDefaultUserAsync(Expression<Func<Author, bool>> expression, bool trackChanges)
    => await FirstOrDefaultAsync(expression, trackChanges);
}