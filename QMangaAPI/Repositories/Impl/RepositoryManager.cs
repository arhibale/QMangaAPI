using QMangaAPI.Repositories.Context;

namespace QMangaAPI.Repositories.Impl;

public class RepositoryManager : IRepositoryManager
{
  private IUserRepository? userRepository;
  private ITagRepository? tagRepository;
  private IRoleRepository? roleReposiotry;
  private IImageRepository? imageRepository;
  private IBookTypeRepository? bookTypeRepository;
  private IBookRepository? bookRepository;
  private IAuthorRepository? authorRepository;
  private IArtistRepository? artistRepository;

  private readonly AppDbContext context;

  public RepositoryManager(AppDbContext context)
  {
    this.context = context;
  }

  public IUserRepository Users => userRepository ??= new UserRepository(context);
  public ITagRepository Tags => tagRepository ??= new TagRepository(context);
  public IRoleRepository Roles => roleReposiotry ??= new RoleRepository(context);
  public IImageRepository Images => imageRepository ??= new ImageRepository(context);
  public IBookTypeRepository BookTypes => bookTypeRepository ??= new BookTypeRepository(context);
  public IBookRepository Books => bookRepository ??= new BookRepository(context);
  public IAuthorRepository Authors => authorRepository ??= new AuthorRepository(context);
  public IArtistRepository Artists => artistRepository ??= new ArtistRepository(context);

  public void Save() => context.SaveChanges();
}