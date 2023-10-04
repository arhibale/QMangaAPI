using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using QMangaAPI.Models;
using QMangaAPI.Models.Impl;
using QMangaAPI.Repositories.Context;

namespace QMangaAPI.Repositories.Impl;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
  public UserRepository(AppDbContext context) : base(context)
  {
  }

  public void CreateUser(User user) => Create(user);

  public void UpdateUser(User user) => Update(user);

  public async Task<bool> AnyUserAsync(Expression<Func<User, bool>> expression, bool trackChanges)
    => await AnyAsync(expression, trackChanges);

  public async Task<User?> FirstOrDefaultUserAsync(Expression<Func<User, bool>> expression, bool trackChanges)
    => await FirstOrDefaultAsync(expression, trackChanges);

  public async Task<User?> FirstOrDefaultUserIncludeModelAsync(Expression<Func<User, IEntity>> include,
    Expression<Func<User, bool>> expression, bool trackChanges)
    => !trackChanges
      ? await context.Users
        .AsNoTracking()
        .Include(include)
        .FirstOrDefaultAsync(expression)
      : await context.Users
        .Include(include)
        .FirstOrDefaultAsync(expression);
}