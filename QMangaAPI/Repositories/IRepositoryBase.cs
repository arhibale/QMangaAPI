using System.Linq.Expressions;

namespace QMangaAPI.Repositories;

/// <summary>
/// Репозиторий.
/// </summary>
/// <typeparam name="T">Сущность хранящиеся в репозитории.</typeparam>
public interface IRepositoryBase<T>
{
  /// <summary>
  /// Найти все в репозитории..
  /// </summary>
  /// <returns>Список сущностей.</returns>
  IQueryable<T> FindAll();
  
  /// <summary>
  /// Найти сущности.
  /// </summary>
  /// <param name="expression">Условие нахождения.</param>
  /// <returns>Список сущностей.</returns>
  IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
  
  /// <summary>
  /// Найти сущность.
  /// </summary>
  /// <param name="expression">Условие нахождения.</param>
  /// <returns>Сущность.</returns>
  Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
  
  /// <summary>
  /// Хранится ли что-нибудь в репозитории.
  /// </summary>
  /// <param name="expression">Условие.</param>
  /// <returns>Результат проверки.</returns>
  Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
  
  /// <summary>
  /// Сохранить сущность.
  /// </summary>
  /// <param name="entity">Сущность.</param>
  void Create(T entity);
  
  /// <summary>
  /// Обновить сущность.
  /// </summary>
  /// <param name="entity">Сущность.</param>
  void Update(T entity);
  
  /// <summary>
  /// Удалить сущность.
  /// </summary>
  /// <param name="entity">Сущность.</param>
  void Delete(T entity);
}