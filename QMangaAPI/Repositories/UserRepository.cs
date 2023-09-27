using Microsoft.EntityFrameworkCore;
using QMangaAPI.Data.Context;
using QMangaAPI.Data.Interfaces.Repositories;
using QMangaAPI.Models;

namespace QMangaAPI.Repositories;

public class UserRepository : IUserRepository
{
  private readonly AppDbContext context;

  public UserRepository(AppDbContext context)
  {
    this.context = context;
  }

  public async Task<IEnumerable<User>> GetAllAsync()
  {
    return await context.Users.ToListAsync();
  }

  public async Task<User?> GetByIdAsync(Guid id)
  {
    return await context.Users.FirstOrDefaultAsync(e => e.Id == id);
  }
  
  public async Task<User?> GetByUsernameAsync(string username)
  {
    return await context.Users
      .Include(e => e.Role)
      .FirstOrDefaultAsync(e => e.Username == username);
  }

  public async Task<bool> AnyRefreshTokenAsync(string refreshToken)
  {
    return await context.Users.AnyAsync(e => e.RefreshToken == refreshToken);
  }

  public async Task<User?> GetByEmailAsync(string email)
  {
    return await context.Users.FirstOrDefaultAsync(e => e.Email == email);
  }

  public async Task<bool> AddAsync(User entity)
  {
    await context.Users.AddAsync(entity);
    return await SaveAllAsync();
  }

  public async Task<bool> UpdateAsync(User entity)
  {
    context.Users.Update(entity);
    return await SaveAllAsync();
  }

  public async Task<bool> DeleteAsync(User entity)
  { 
    context.Users.Remove(entity);
    return await SaveAllAsync();
  }

  public async Task<bool> SaveAllAsync()
  {
    return await context.SaveChangesAsync() > 0;
  }
}