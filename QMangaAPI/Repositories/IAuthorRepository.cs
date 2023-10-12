using System.Linq.Expressions;
using QMangaAPI.Models.Impl;

namespace QMangaAPI.Repositories;

public interface IAuthorRepository
{
  IQueryable<Author> GetAll(bool trackChanges);
  Task<Author?> FirstOrDefaultUserAsync(Expression<Func<Author, bool>> expression, bool trackChanges);
}