using QMangaAPI.Models;

namespace QMangaAPI.Data.Interfaces.Repositories;

public interface IUserRepository : IRepository<User, Guid>
{
  Task<User?> GetByUsernameAsync(string username);
  Task<bool> AnyRefreshTokenAsync(string refreshToken);
  Task<User?> GetByEmailAsync(string email);
}