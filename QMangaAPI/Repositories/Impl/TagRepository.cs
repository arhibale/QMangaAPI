using QMangaAPI.Models.Impl;
using QMangaAPI.Repositories.Context;

namespace QMangaAPI.Repositories.Impl;

public class TagRepository : RepositoryBase<Tag>, ITagRepository
{
  public TagRepository(AppDbContext context) : base(context)
  {
  }
}