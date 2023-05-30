using DataAccess.Contexts;
using DomainModel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected AppDbContext Context { get; set; }

    public GenericRepository(AppDbContext context)
    {
        Context = context;
    }

    public async Task<T> Create(T entity)
    {
        var rowData = await Context.Set<T>().AddAsync(entity);
        var isRowEffected = await Context.SaveChangesAsync();
        //if (isRowEffected > 0)
        //    return null;
        return rowData.Entity;
    }

    public async Task<IEnumerable<T>> ReadAll(Expression<Func<T, bool>> expression, string[] includes = null!)
    {
        IQueryable<T> query = Context.Set<T>();

        if (includes != null)
            foreach (var item in includes)
                query = query.Include(item);
        return await query.ToListAsync();

    }

    public async Task<IEnumerable<T>> ReadAll(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = Context.Set<T>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> ReadAll(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            int? page, int? pageSize,
            params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = Context.Set<T>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        if (page.HasValue && pageSize.HasValue)
        {
            int skip = (page.Value - 1) * pageSize.Value;
            query = query.Skip(skip).Take(pageSize.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<T?> ReadSingleById(Expression<Func<T, bool>> expression, Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = Context.Set<T>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.SingleOrDefaultAsync(expression);
    }
    public async Task<T?> ReadSingle(int id)
    {
        return await Context.Set<T>().FindAsync(id);
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
