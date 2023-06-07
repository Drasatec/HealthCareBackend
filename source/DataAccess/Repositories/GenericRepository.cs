using DataAccess.Contexts;
using DomainModel.Interfaces;
using DomainModel.Models;
using DomainModel.Models.Hospitals;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repositories;

public class GenericRepository : IGenericRepository// where T : class
{
    protected AppDbContext Context { get; set; }
    public GenericRepository(AppDbContext context) => Context = context;



    public async Task<Response> GenericUpdate<TEntity>(List<TEntity> entity) where TEntity : class
    {
        var res = new Response();
        try
        {
            Context.Set<TEntity>().UpdateRange(entity);
            await Context.SaveChangesAsync();
            res.Success = true;
            return res;
        }
        catch (Exception ex)
        {
            res.Success = false;
            res.Message = "can not duplicate foreignKey with same Id ......." + ex.Message;
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
            if (rowEffict > 0) return new Response(true, $"delete ids of {typeof(TEntity).Name}: {arrayIds} ");

            return new Response(false, $"Ids : {arrayIds} is not found."); ;
        }
        catch (Exception ex)
        {
            return new Response(false, ex.Message);
        }
    }

    public async Task<Response> GenericUpdateSinglePropertyById<TEntity>(int id, TEntity entity, Expression<Func<TEntity, bool>> propertyExpression) where TEntity : class
    {
        try
        {
            Context.Attach(entity);
            Context.Entry(entity).Property(propertyExpression).IsModified = true;
            var rowEffected = await Context.SaveChangesAsync();
            if (rowEffected > 0)
                return new Response(true, $"Update on entity with Id: {id}");
            return new Response(false, $"Entity with Id: {id} not found");
        }
        catch (Exception ex)
        {
            return new Response(false, $"No changes on entity with Id: {id} , ..." + ex.Message);
        }
    }

    public async Task<IEnumerable<TEntity>> GenericReadAll<TEntity>(
                                       Expression<Func<TEntity, bool>> filter,
                                       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy,
                                       int? page, int? pageSize,
                                       Expression<Func<TEntity, TEntity>> selectExpression,
                                       params Expression<Func<TEntity, object>>[]? includes) where TEntity : class
    {
        IQueryable<TEntity> query = Context.Set<TEntity>();

        if (filter != null)
        {
            query = query.Where(filter);
        }
        if(includes != null)
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

        return await query.Select(selectExpression).ToListAsync();
    }
    // filter and select
    public async Task<IEnumerable<TEntity>> GenericReadAll<TEntity>(Expression<Func<TEntity, bool>> filter,Expression<Func<TEntity, TEntity>> selectExpression) where TEntity : class
    {
        IQueryable<TEntity> query = Context.Set<TEntity>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.Select(selectExpression).ToListAsync();
    }


    // this func writed by chat
    void test()
    {

        //    try
        //    {
        //        var current = await Context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        //        if (current != null)
        //        {
        //            var propertyInfo = (propertyExpression.Body as MemberExpression)?.Member as PropertyInfo;
        //            if (propertyInfo != null && propertyInfo.PropertyType == typeof(bool))
        //            {
        //                propertyInfo.SetValue(current, propertyValue);
        //                Context.Attach(entity);
        //                Context.Entry(entity).Property(propertyExpression).IsModified = true;
        //                var rowEffected = await Context.SaveChangesAsync();
        //                if (rowEffected > 0)
        //                    return new Response(true, $"Update on entity with Id: {id}");
        //            }
        //        }
        //        return new Response(false, $"Entity with Id: {id} not found");
        //    }
        //    catch (Exception)
        //    {
        //        return new Response(false, $"No changes on entity with Id: {id}");
        //    }
    }
















    //public async Task<T> Create(T entity)
    //{
    //    var rowData = await Context.Set<T>().AddAsync(entity);
    //    await Context.SaveChangesAsync();
    //    return rowData.Entity;
    //}

    //public async Task<IEnumerable<T>> ReadAll(Expression<Func<T, bool>> expression, string[] includes = null!)
    //{
    //    IQueryable<T> query = Context.Set<T>();

    //    if (includes != null)
    //        foreach (var item in includes)
    //            query = query.Include(item);
    //    return await query.ToListAsync();

    //}

    //public async Task<IEnumerable<T>> ReadAll(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
    //{
    //    IQueryable<T> query = Context.Set<T>();

    //    if (filter != null)
    //    {
    //        query = query.Where(filter);
    //    }

    //    foreach (var include in includes)
    //    {
    //        query = query.Include(include);
    //    }

    //    return await query.ToListAsync();
    //}

    //public async Task<IEnumerable<T>> ReadAll(
    //        Expression<Func<T, bool>> filter,
    //        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
    //        int? page, int? pageSize,
    //        params Expression<Func<T, object>>[] includes)
    //{
    //    IQueryable<T> query = Context.Set<T>();

    //    if (filter != null)
    //    {
    //        query = query.Where(filter);
    //    }

    //    foreach (var include in includes)
    //    {
    //        query = query.Include(include);
    //    }

    //    if (orderBy != null)
    //    {
    //        query = orderBy(query);
    //    }

    //    if (page.HasValue && pageSize.HasValue)
    //    {
    //        int skip = (page.Value - 1) * pageSize.Value;
    //        query = query.Skip(skip).Take(pageSize.Value);
    //    }

    //    return await query.ToListAsync();
    //}

    //public async Task<T?> ReadSingleById(Expression<Func<T, bool>> expression, Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
    //{
    //    IQueryable<T> query = Context.Set<T>();

    //    if (filter != null)
    //    {
    //        query = query.Where(filter);
    //    }

    //    foreach (var include in includes)
    //    {
    //        query = query.Include(include);
    //    }

    //    return await query.SingleOrDefaultAsync(expression);
    //}
    //public async Task<T?> ReadSingle(int id)
    //{
    //    return await Context.Set<T>().FindAsync(id);
    //}


    //public Task<IEnumerable<T>> Finde(Expression<Func<T, bool>> expression, int? take, int? skip)
    //{
    //    throw new NotImplementedException();
    //}

}
