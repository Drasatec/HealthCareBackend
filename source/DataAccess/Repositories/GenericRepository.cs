using DataAccess.Contexts;
using DomainModel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected AppDbContext Context { get; set; }

        public GenericRepository(AppDbContext context)
        {
            Context = context;
        }


        public async Task<T> Create(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> ReadAll(Expression<Func<T, bool>> expression, string[] includes = null!)
        {
            IQueryable<T> query = Context.Set<T>();

            if (includes != null)
                foreach (var item in includes)
                    query = query.Include(item);
            return await query.ToListAsync();
        }


        public async Task<T?> ReadById(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<T?> ReadById(Expression<Func<T, bool>> expression, string include = null!)
        {
            IQueryable<T> query = Context.Set<T>();

            if (include != null)
                query = query.Include(include);

            //if (includes != null)
            //    foreach (var item in includes)
            //        query = query.Include(item);

            return await query.SingleOrDefaultAsync(expression);
        }


        public Task<IEnumerable<T>> Finde(Expression<Func<T, bool>> expression, int? take, int? skip)
        {
            throw new NotImplementedException();
        }
        // delete
        // update
        // getAll
        // and so on

    }
}
