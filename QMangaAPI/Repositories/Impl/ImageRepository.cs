using QMangaAPI.Models.Impl;
using QMangaAPI.Repositories.Context;

namespace QMangaAPI.Repositories.Impl;

public class ImageRepository : RepositoryBase<Image>, IImageRepository
{
  public ImageRepository(AppDbContext context) : base(context)
  {
  }
}