using QMangaAPI.Models.Impl;

namespace QMangaAPI.Services;

public interface IImageService
{
  Task<List<Image>> SaveImagesAsync(List<IFormFile> imgFiles, string bookName);
  Task<CoverImage> SaveImageAsync(IFormFile imgFile, string bookName);
}