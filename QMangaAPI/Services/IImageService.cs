using QMangaAPI.Models.Impl;
using Image = QMangaAPI.Models.Impl.Image;

namespace QMangaAPI.Services;

/// <summary>
/// Сервис для работы с изображениями.
/// </summary>
public interface IImageService
{
  /// <summary>
  /// Сохранить список изображений.
  /// </summary>
  /// <param name="imgFiles">Список изобрадений.</param>
  /// <param name="bookName">Имя манги.</param>
  /// <returns>Список моделей изображений.</returns>
  Task<List<Image>> SaveImagesAsync(List<IFormFile> imgFiles, string bookName);
  
  /// <summary>
  /// Сохранить изображение.
  /// </summary>
  /// <param name="imgFile">Файл изображения.</param>
  /// <param name="bookName">Имя манги.</param>
  /// <returns>Модель изображения.</returns>
  Task<CoverImage> SaveImageAsync(IFormFile imgFile, string bookName);
}