using System.Linq.Expressions;
using Image = QMangaAPI.Models.Impl.Image;

namespace QMangaAPI.Repositories;

/// <summary>
/// Репозиторий изображений.
/// </summary>
public interface IImageRepository
{
  /// <summary>
  /// Найти изображения.
  /// </summary>
  /// <param name="expression">Условие.</param>
  /// <returns>Список изображений.</returns>
  public IQueryable<Image> FindByCondition(Expression<Func<Image, bool>> expression);
}