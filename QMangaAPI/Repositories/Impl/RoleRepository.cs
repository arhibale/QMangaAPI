using System.Linq.Expressions;
using QMangaAPI.Models.Impl;
using QMangaAPI.Repositories.Context;

namespace QMangaAPI.Repositories.Impl;

public class RoleRepository : RepositoryBase<Role>, IRoleRepository
{
  public RoleRepository(AppDbContext context) : base(context)
  {
  }

  public async Task<Role?> FirstOrDefaultRolesAsync(Expression<Func<Role, bool>> expression)
    => await FirstOrDefaultAsync(expression);
}