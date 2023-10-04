namespace QMangaAPI.Repositories;

public interface IRepositoryManager
{
  IUserRepository Users { get; }
  ITagRepository Tags { get; }
  IRoleRepository Roles { get; }
  IImageRepository Images { get; }
  IBookTypeRepository BookTypes { get; }
  IBookRepository Books { get; }
  IAuthorRepository Authors { get; }
  IArtistRepository Artists { get; }

  void Save();
}