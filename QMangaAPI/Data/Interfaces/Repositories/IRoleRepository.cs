using QMangaAPI.Data.Enums;
using QMangaAPI.Models;

namespace QMangaAPI.Data.Interfaces.Repositories;

public interface IRoleRepository : IRepository<Role, Guid>
{
  Task<Role?> GetByNameAsync(RoleEnums toString);
}