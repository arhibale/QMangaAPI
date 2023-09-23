using QMangaAPI.Data.Context;
using QMangaAPI.Data.Enums;
using QMangaAPI.Models;

namespace QMangaAPI.Data;

public static class Seed
{
  public static void SeedData(IApplicationBuilder applicationBuilder)
  {
    using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
    var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

    context.Database.EnsureCreated();
    
    if (context.BookTypes.Any() && context.Roles.Any()) return;
    
    context.Roles.AddRangeAsync(new List<Role>
    {
      new() { Name = RoleEnums.User.ToString(), CreatedAt = DateTime.Now, UpdatedIn = DateTime.Now },
      new() { Name = RoleEnums.Admin.ToString(), CreatedAt = DateTime.Now, UpdatedIn = DateTime.Now },
      new() { Name = RoleEnums.Creator.ToString(), CreatedAt = DateTime.Now, UpdatedIn = DateTime.Now }
    });

    context.BookTypes.AddRangeAsync(new List<BookType>
    {
      new() { Name = BookTypeEnum.Manga.ToString(), CreatedAt = DateTime.Now, UpdatedIn = DateTime.Now },
      new() { Name = BookTypeEnum.Manhwa.ToString(), CreatedAt = DateTime.Now, UpdatedIn = DateTime.Now },
      new() { Name = BookTypeEnum.Manhua.ToString(), CreatedAt = DateTime.Now, UpdatedIn = DateTime.Now },
      new() { Name = BookTypeEnum.Comics.ToString(), CreatedAt = DateTime.Now, UpdatedIn = DateTime.Now },
    });
      
    context.SaveChangesAsync();
  }
}