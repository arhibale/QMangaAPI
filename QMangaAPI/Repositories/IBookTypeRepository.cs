using System.Linq.Expressions;
using QMangaAPI.Models.Impl;

namespace QMangaAPI.Repositories;

public interface IBookTypeRepository
{
  Task<BookType?> FirstOrDefaultUserAsync(Expression<Func<BookType, bool>> expression, bool trackChanges);
}