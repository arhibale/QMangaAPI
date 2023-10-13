using System.Linq.Expressions;
using QMangaAPI.Models.Impl;

namespace QMangaAPI.Repositories;

/// <summary>
/// Репозиторий ролей.
/// </summary>
public interface IRoleRepository
{
  /// <summary>
  /// Получить роль.
  /// </summary>
  /// <param name="expression">Условие получения.</param>
  /// <returns>Роль.</returns>
  Task<Role?> FirstOrDefaultRolesAsync(Expression<Func<Role, bool>> expression);
}