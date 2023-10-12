using System.Linq.Expressions;
using QMangaAPI.Models.Impl;

namespace QMangaAPI.Repositories;

public interface IArtistRepository
{
  IQueryable<Artist> GetAll(bool trackChanges);
  Task<Artist?> FirstOrDefaultUserAsync(Expression<Func<Artist, bool>> expression, bool trackChanges);
}