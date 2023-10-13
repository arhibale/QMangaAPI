using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using QMangaAPI.Models;
using QMangaAPI.Repositories.Context;

namespace QMangaAPI.Repositories.Impl;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class, IEntity
{
  protected readonly AppDbContext context;

  protected RepositoryBase(AppDbContext context)
  {
    this.context = context;
  }

  public IQueryable<T> FindAll() => context.Set<T>();

  public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => 
    context.Set<T>()
        .Where(expression);

  public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression) =>
    context.Set<T>()
        .FirstOrDefaultAsync(expression);

  public Task<bool> AnyAsync(Expression<Func<T, bool>> expression) =>
    context.Set<T>()
        .AnyAsync(expression);

  public void Create(T entity) => context.Set<T>().Add(entity);

  public void Update(T entity)
  {
    entity.UpdatedIn = DateTime.Now;
    context.Set<T>().Update(entity);
  } 

  public void Delete(T entity) => context.Set<T>().Remove(entity);
}