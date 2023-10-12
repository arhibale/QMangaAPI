using System.Linq.Expressions;
using QMangaAPI.Models.Impl;
using QMangaAPI.Repositories.Context;

namespace QMangaAPI.Repositories.Impl;

public class ArtistRepository : RepositoryBase<Artist>, IArtistRepository
{
  public ArtistRepository(AppDbContext context) : base(context)
  {
  }
  
  public IQueryable<Artist> GetAll(bool trackChanges) =>
    FindAll(trackChanges);
  
  public async Task<Artist?> FirstOrDefaultUserAsync(Expression<Func<Artist, bool>> expression, bool trackChanges)
    => await FirstOrDefaultAsync(expression, trackChanges);
}