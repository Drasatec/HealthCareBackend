using DataAccess.Contexts;
using DomainModel.Entities;
using DomainModel.Interfaces;
using DomainModel.Models;
using DomainModel.Models.Hospitals;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected AppDbContext Context { get; set; }

    public GenericRepository(AppDbContext context) => Context = context;


    public async Task<Response<HospitalDto?>> GAddTranslations<TEntity>(List<TEntity> entity) where TEntity : class
    {
        var res = new Response<HospitalDto?>();
        try
        {
            await Context.Set<TEntity>().AddRangeAsync(entity);
            await Context.SaveChangesAsync();
            res.Success = true;
            return res;
        }
        catch (Exception ex)
        {
            res.Success = false;
            res.Message = "can not duplicate langCode with same hosId ......." + ex.Message;
            return res;
        }
    }

    public async Task<Response> GenericDelete<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> expression, params int[] ids) where TEntity : class
    {
        var arrayIds = string.Join(" ", ids);
        try
        {
            var current = await Context.Set<TEntity>().Where(expression).ToListAsync();
            if (current != null)
            {
                Context.RemoveRange(current);
            }
            var rowEffict = await Context.SaveChangesAsync();
            if (rowEffict > 0) return new Response(true, $"delete translate: ids of hosId: {arrayIds} ");

            return new Response(false, $"Id : {arrayIds} is not found  "); ;
        }
        catch (Exception ex)
        {
            return new Response(false, ex.Message);
        }
    }


















    public async Task<T> Create(T entity)
    {
        var rowData = await Context.Set<T>().AddAsync(entity);
        await Context.SaveChangesAsync();
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
