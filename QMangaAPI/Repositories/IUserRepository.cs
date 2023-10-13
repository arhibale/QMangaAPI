using System.Linq.Expressions;
using QMangaAPI.Models;
using QMangaAPI.Models.Impl;

namespace QMangaAPI.Repositories;

/// <summary>
/// Репозиторий пользователей.
/// </summary>
public interface IUserRepository
{
  /// <summary>
  /// Создать пользователя.
  /// </summary>
  /// <param name="user">Пользователь.</param>
  void CreateUser(User user);
  
  /// <summary>
  /// Обновить пользователя.
  /// </summary>
  /// <param name="user">Пользователь.</param>
  void UpdateUser(User user);
  
  /// <summary>
  /// Хранится ли пользователь в репозитории.
  /// </summary>
  /// <param name="expression">Условие.</param>
  /// <returns>Результат проверки.</returns>
  Task<bool> AnyUserAsync(Expression<Func<User, bool>> expression);
  
  /// <summary>
  /// Получить пользователя.
  /// </summary>
  /// <param name="expression">Услоовие.</param>
  /// <returns>Пользователь.</returns>
  Task<User?> FirstOrDefaultUserAsync(Expression<Func<User, bool>> expression);
  
  /// <summary>
  /// Получить пользователя.
  /// </summary>
  /// <param name="include">Подключить ещё одну таблицу.</param>
  /// <param name="expression">Условие.</param>
  /// <returns>Пользователь.</returns>
  Task<User?> FirstOrDefaultUserIncludeModelAsync(Expression<Func<User, IEntity>> include, 
    Expression<Func<User, bool>> expression);
}