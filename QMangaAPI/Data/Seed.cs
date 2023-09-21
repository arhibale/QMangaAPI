using QMangaAPI.Data.Context;

namespace QMangaAPI.Data;

public static class Seed
{
  public static void SeedData(IApplicationBuilder applicationBuilder)
  {
    using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
    var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
      
    context.Database.EnsureCreated();
  }
}