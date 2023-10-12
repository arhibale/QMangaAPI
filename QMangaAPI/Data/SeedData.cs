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

    await context.Tags.AddRangeAsync(new List<Tag>
      {
        new() { Name = "Tag1" },
        new() { Name = "Tag2" },
        new() { Name = "Tag3" },
        new() { Name = "Tag4" },
        new() { Name = "Tag5" },
        new() { Name = "Tag6" },
        new() { Name = "Tag7" },
        new() { Name = "Tag8" },
        new() { Name = "Tag9" },
        new() { Name = "Tag10" },
        new() { Name = "Tag11" },
        new() { Name = "Tag12" },
        new() { Name = "Tag13" },
        new() { Name = "Tag14" },
        new() { Name = "Tag15" },
        new() { Name = "Tag16" },
        new() { Name = "Tag17" },
        new() { Name = "Tag18" },
        new() { Name = "Tag19" },
        new() { Name = "Tag20" },
        new() { Name = "Tag21" },
        new() { Name = "Tag22" },
        new() { Name = "Tag23" },
        new() { Name = "Tag24" },
        new() { Name = "Tag25" },
        new() { Name = "Tag26" },
        new() { Name = "Tag27" },
        new() { Name = "Tag28" },
        new() { Name = "Tag29" }
      }
    );

    await context.Authors.AddRangeAsync(new List<Author>
      {
        new() { Name = "Author" },
        new() { Name = "Artem" },
        new() { Name = "Ninju" },
        new() { Name = "Kalen" }
      }
    );

    await context.Artists.AddRangeAsync(new List<Artist>
      {
        new() { Name = "Mr. San" },
        new() { Name = "Polqr" },
        new() { Name = "ArhiBale" },
        new() { Name = "Kekya" }
      }
    );

    await context.BookTypes.AddRangeAsync(new List<BookType>
      {
        new() { Name = "Manga" },
        new() { Name = "Manhwa" },
        new() { Name = "Manhua" },
        new() { Name = "Comics" }
      }
    );

    await context.SaveChangesAsync();
  }
}