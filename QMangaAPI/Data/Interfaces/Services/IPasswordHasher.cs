namespace QMangaAPI.Data.Interfaces.Services;

public interface IPasswordHasher
{
  string Hash(string password);
  bool Verify(string passwordHash, string inputPassword);
}