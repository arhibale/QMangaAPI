using QMangaAPI.Data.Enum;
using QMangaAPI.Models.Impl;
using QMangaAPI.Repositories.Context;
using QMangaAPI.Services;

namespace QMangaAPI.Data;

public static class SeedData
{
  public static async void Seed(IApplicationBuilder applicationBuilder)
  {
    using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
    var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
    var ph = serviceScope.ServiceProvider.GetService<IPasswordHasherService>();

    if (context is null || ph is null) throw new InvalidOperationException("context is null!");

    await context.Database.EnsureCreatedAsync();

    if (context.Users.Any()) return;

    var user = new Role { Name = RoleEnum.User.ToString(), CreatedAt = DateTime.Now, UpdatedIn = DateTime.Now };
    var admin = new Role { Name = RoleEnum.Admin.ToString(), CreatedAt = DateTime.Now, UpdatedIn = DateTime.Now };
    var creator = new Role { Name = RoleEnum.Creator.ToString(), CreatedAt = DateTime.Now, UpdatedIn = DateTime.Now };

    await context.Roles.AddRangeAsync(new List<Role> { user, admin, creator });

    await context.Users.AddRangeAsync(new List<User>
      {
        new() { Username = "user1", Email = "arhibale@yandex.ru", Password = ph.Hash("Lololohka1<>"), Role = user }, 
        new() { Username = "user2", Email = "arhibale@yandex.ru", Password = ph.Hash("Lololohka1<>"), Role = admin },
        new() { Username = "user3", Email = "arhibale@yandex.ru", Password = ph.Hash("Lololohka1<>"), Role = creator }
      }
    );

    await context.SaveChangesAsync();
  }
}