using System.Linq.Expressions;

namespace Machines.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllWithRelationshipAsync(Expression<Func<T, object>> criteria);
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    T FindWithRelationship(Expression<Func<T, bool>> expression, Expression<Func<T, object>> criteria);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}
}