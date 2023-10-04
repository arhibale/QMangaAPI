using QMangaAPI.Models.Impl;
using QMangaAPI.Repositories.Context;

namespace QMangaAPI.Repositories.Impl;

public class ArtistRepository : RepositoryBase<Artist>, IArtistRepository
{
  public ArtistRepository(AppDbContext context) : base(context)
  {
  }
}