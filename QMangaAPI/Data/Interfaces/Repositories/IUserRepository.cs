using QMangaAPI.Models;

namespace QMangaAPI.Data.Interfaces.Repositories;

public interface IUserRepository : IRepository<User, Guid>
{
  Task<User?> GetByUsernameAsync(string username);
}