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
        new() { Username = "Kalen", Email = "arhibale@yandex.ru", Password = ph.Hash("Lololohka1<>"), Role = user },
        new() { Username = "Kekya", Email = "arhibale2@gmail.com", Password = ph.Hash("Lololohka1<>"), Role = admin },
        new() { Username = "ArhiBale", Email = "arhibale@yandex.ru", Password = ph.Hash("Lololohka1<>"), Role = creator }
      }
    );

    await context.Tags.AddRangeAsync(new List<Tag>
      {
        new() { Name = "Art" },
        new() { Name = "Action" },
        new() { Name = "Movie" },
        new() { Name = "Martial arts" },
        new() { Name = "Vampires" },
        new() { Name = "Harem" },
        new() { Name = "Gender intrigue" },
        new() { Name = "Heroic fantasy" },
        new() { Name = "Detective" },
        new() { Name = "Josei" },
        new() { Name = "Drama" },
        new() { Name = "Game" },
        new() { Name = "Isekai" },
        new() { Name = "History" },
        new() { Name = "Cyberpunk" },
        new() { Name = "Codomoko" },
        new() { Name = "Mediyamaho-shojo" },
        new() { Name = "Meha" },
        new() { Name = "Mysticism" },
        new() { Name = "Science fiction" },
        new() { Name = "Omegavers" },
        new() { Name = "Everyday life" },
        new() { Name = "Post apocalyptic" },
        new() { Name = "Adventures" },
        new() { Name = "Psychology" },
        new() { Name = "Romance" },
        new() { Name = "Supernatural" },
        new() { Name = "Fantastic" },
        new() { Name = "Fantasy" },
        new() { Name = "School" }
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