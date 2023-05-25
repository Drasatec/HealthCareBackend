using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Create(T entity);
        Task<T?> ReadById(int id);
        Task<T?> ReadById(Expression<Func<T, bool>> expression, string include=null!);
        Task<IEnumerable<T>> Finde(Expression<Func<T, bool>> expression, int? take, int? skip);
        Task<IEnumerable<T>> ReadAll(Expression<Func<T, bool>> expression, string[] include = null!);
    }
}
