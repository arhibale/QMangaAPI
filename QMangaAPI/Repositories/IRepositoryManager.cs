namespace QMangaAPI.Repositories;

/// <summary>
/// Менеджер репозиторий.
/// </summary>
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

  /// <summary>
  /// Сохранить все изменения в БД.
  /// </summary>
  void Save();
}