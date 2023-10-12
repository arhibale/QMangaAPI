using System.Linq.Expressions;
using QMangaAPI.Models.Impl;
using QMangaAPI.Repositories.Context;

namespace QMangaAPI.Repositories.Impl;

public class TagRepository : RepositoryBase<Tag>, ITagRepository
{
  public TagRepository(AppDbContext context) : base(context)
  {
  }

  public IQueryable<Tag> GetAll(bool trackChanges) =>
    FindAll(trackChanges);
  
  public async Task<Tag?> FirstOrDefaultUserAsync(Expression<Func<Tag, bool>> expression, bool trackChanges)
    => await FirstOrDefaultAsync(expression, trackChanges);
}