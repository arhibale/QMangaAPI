using QMangaAPI.Data.Enums;

namespace QMangaAPI.Data.Interfaces.Services;

public interface IJwtGenerator
{
  string CreateJwt(string username, string role);
}