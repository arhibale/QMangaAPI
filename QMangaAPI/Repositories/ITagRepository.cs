using System.Linq.Expressions;
using QMangaAPI.Models.Impl;

namespace QMangaAPI.Repositories;

public interface ITagRepository
{
  IQueryable<Tag> GetAll(bool trackChanges);
  Task<Tag?> FirstOrDefaultUserAsync(Expression<Func<Tag, bool>> expression, bool trackChanges);
}