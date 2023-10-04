using QMangaAPI.Models.Impl;

namespace QMangaAPI.Repositories;

public interface IBookRepository
{
  Task<int> GetCountAsync();
  Task<List<Book>> GetPageAsync(int? page, int pageSize);
}