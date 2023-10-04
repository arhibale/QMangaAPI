using QMangaAPI.Models.Impl;

namespace QMangaAPI.Services;

public interface IImageService
{
  Task<Image> SaveImageAsync(IFormFile imgFile, string bookName);
}