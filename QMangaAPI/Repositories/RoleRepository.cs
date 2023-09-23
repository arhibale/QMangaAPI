using Microsoft.EntityFrameworkCore;
using QMangaAPI.Data.Context;
using QMangaAPI.Data.Enums;
using QMangaAPI.Data.Interfaces.Repositories;
using QMangaAPI.Models;

namespace QMangaAPI.Repositories;

public class RoleRepository : IRoleRepository
{
  private readonly AppDbContext context;

  public RoleRepository(AppDbContext context)
  {
    this.context = context;
  }

  public async Task<IEnumerable<Role>> GetAllAsync()
  {
    return await context.Roles.ToListAsync();
  }

  public async Task<Role?> GetByIdAsync(Guid id)
  {
    return await context.Roles.FirstOrDefaultAsync(e => e.Id == id);
  }
  
  public async Task<Role?> GetByNameAsync(RoleEnums name)
  {
    return await context.Roles.FirstOrDefaultAsync(e => e.Name == name.ToString());
  }

  public async Task<bool> AddAsync(Role entity)
  {
    await context.Roles.AddAsync(entity);
    return await SaveAllAsync();
  }

  public async Task<bool> UpdateAsync(Role entity)
  {
    context.Roles.Update(entity);
    return await SaveAllAsync();
  }

  public async Task<bool> DeleteAsync(Role entity)
  { 
    context.Roles.Remove(entity);
    return await SaveAllAsync();
  }

  public async Task<bool> SaveAllAsync()
  {
    return await context.SaveChangesAsync() > 0;
  }
}