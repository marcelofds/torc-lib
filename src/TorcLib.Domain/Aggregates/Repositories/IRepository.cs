using System.Linq.Expressions;

namespace TorcLib.Domain.Aggregates.Repositories;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllByExpressionAsync(Expression<Func<T, bool>> predicate);
    Task<T?> GetByIdAsync(int id);
    void Insert(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task SaveAsync();
}