using System.Linq.Expressions;

namespace QMangaAPI.Repositories;

public interface IRepositoryBase<T>
{
  IQueryable<T> FindAll(bool trackChanges);
  IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
  Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, bool trackChanges);
  Task<bool> AnyAsync(Expression<Func<T, bool>> expression, bool trackChanges);
  void Create(T entity);
  void Update(T entity);
  void Delete(T entity);
}