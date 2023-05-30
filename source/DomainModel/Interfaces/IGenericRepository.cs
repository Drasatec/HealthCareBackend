using System.Linq.Expressions;

namespace DomainModel.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Create(T entity);
        Task<T?> ReadSingle(int id);
        Task<T?> ReadSingleById(Expression<Func<T, bool>> expression, Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> Finde(Expression<Func<T, bool>> expression, int? take, int? skip);
        Task<IEnumerable<T>> ReadAll(Expression<Func<T, bool>> expression, string[] include = null!);
        Task<IEnumerable<T>> ReadAll(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> ReadAll(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int? page, int? pageSize, params Expression<Func<T, object>>[] includes);
    }
}
