using System.Linq.Expressions;
using QMangaAPI.Models;
using QMangaAPI.Models.Impl;

namespace QMangaAPI.Repositories;

public interface IUserRepository
{
  void CreateUser(User user);
  void UpdateUser(User user);
  Task<bool> AnyUserAsync(Expression<Func<User, bool>> expression, bool trackChanges);
  Task<User?> FirstOrDefaultUserAsync(Expression<Func<User, bool>> expression, bool trackChanges);

  Task<User?> FirstOrDefaultUserIncludeModelAsync(Expression<Func<User, IEntity>> include,
    Expression<Func<User, bool>> expression, bool trackChanges);
}