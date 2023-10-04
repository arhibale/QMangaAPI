using System.Linq.Expressions;
using QMangaAPI.Models.Impl;

namespace QMangaAPI.Repositories;

public interface IRoleRepository
{
  Task<Role?> FirstOrDefaultRolesAsync(Expression<Func<Role, bool>> expression, bool trackChanges);
}