using System.Linq.Expressions;
using Machines.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Machines.DataAccess.EfCore.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly ApplicationContext _context;
    public GenericRepository(ApplicationContext context)
    {
        _context = context;
    }
    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }
    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _context.Set<T>().AddRangeAsync(entities);
    }
    public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }

    public T FindWithRelationship(Expression<Func<T, bool>> expression, Expression<Func<T, object>> criteria)
    {
        return _context.Set<T>().Where(expression).Include(criteria).FirstOrDefault();
    }
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }
    public async Task<IEnumerable<T>> GetAllWithRelationshipAsync(Expression<Func<T, object>> criteria)
    {
        return await _context.Set<T>().Include(criteria).ToListAsync();
    }
    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }
    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
    public void RemoveRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }
}
}