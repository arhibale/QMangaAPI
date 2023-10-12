using QMangaAPI.Models.Impl;
using QMangaAPI.Repositories.Context;
using Image = QMangaAPI.Models.Impl.Image;

namespace QMangaAPI.Repositories.Impl;

public class ImageRepository : RepositoryBase<Image>, IImageRepository
{
  public ImageRepository(AppDbContext context) : base(context)
  {
  }
}