using DataAccess.Contexts;
using DomainModel.Entities.TranslationModels;
using DomainModel.Helpers;
using DomainModel.Interfaces;
using DomainModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Repositories;

public class GenericRepository : IGenericRepository
{
    protected AppDbContext Context { get; set; }
    public GenericRepository(AppDbContext context) => Context = context;


    // create
    public async Task<Response<TEntity>> GenericCreate<TEntity>(TEntity entity) where TEntity : class
    {
        try
        {
            var result = await Context.Set<TEntity>().AddAsync(entity);

            var row = await Context.SaveChangesAsync();

            if (row > 0)
            {
                return new Response<TEntity>(true, "succcess", result.Entity);
            }

            return new Response<TEntity>(false, "No rows affected");
        }
        catch (Exception ex)
        {
            return new Response<TEntity>(false, ex.Message + "and______" + ex.InnerException?.Message);
        }
    }




    public async Task<Response<object>> GenericCreateWithImage<TEntity>(TEntity tEntity, Stream? image = null) where TEntity : class
    {
        var entity = (dynamic)tEntity;

        try
        {
            if (image != null)
            {
                var imageName = Helper.GenerateImageName();
                _ = DataAccessImageService.SaveSingleImage(image, imageName);
                entity.Photo = imageName;
            }
            else
            {
                entity.Photo = null;
            }

            var result = await Context.Set<TEntity>().AddAsync(entity);

            if (string.IsNullOrEmpty(entity.CodeNumber))
            {
                entity.CodeNumber = "generi- " + Context.Set<TEntity>().Count().ToString();
            }

            var row = await Context.SaveChangesAsync();

            if (row > 0)
            {
                return new Response<object>(true, "id: " + result.Entity.Id);
            }

            return new Response<object>(false, "No rows affected");
        }
        catch (Exception ex)
        {
            return new Response<object>(false, ex.Message);
        }
    }
    // edit
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
            res.Message = ex.Message + "and_____" + ex.InnerException?.Message;
            return res;
        }
    }

    public async Task<Response> GenericUpdate<TEntity>(TEntity entity, params Expression<Func<TEntity, object>>[]? propertiesNotModified) where TEntity : class
    {
        var res = new Response();
        try
        {
            Context.Set<TEntity>().Update(entity);

            if (propertiesNotModified is not null)
                foreach (var prop in propertiesNotModified)
                {
                    Context.Entry(entity).Property(prop).IsModified = false;
                }

            await Context.SaveChangesAsync();
            res.Success = true;
            return res;
        }
        catch (Exception ex)
        {
            res.Success = false;
            res.Message = ex.Message + "and_____" + ex.InnerException?.Message;
            return res;
        }
    }

    public async Task<Response> GenericDelete<TEntity>(Expression<Func<TEntity, bool>> expression, params int[] ids) where TEntity : class
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
            return new Response(false, ex.Message + "and_____" + ex.InnerException?.Message);
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


    // read
    public async Task<PagedResponse<TEntity>?> GenericReadAllWihInclude<TEntity>(
       Expression<Func<TEntity, bool>>? filter,
       Expression<Func<TEntity, object>>? orderBy,
       Expression<Func<TEntity, object>>? include,
       int? page, int? pageSize) where TEntity : class
    {
        IQueryable<TEntity> query = Context.Set<TEntity>();

        int total;
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (orderBy != null)
        {
            query = query.OrderByDescending(orderBy);
            //query = orderBy(query);
        }


        total = query.Count();
        if (total < 0)
            return null!;


        // page size
        GenericPagination(ref query, ref pageSize, ref page, total);

        if (include != null)
            query = query.Include(include);

        PagedResponse<TEntity> all = new()
        {
            Total = total,
            Page = page,
            PageSize = pageSize,
            Data = await query.ToListAsync(),
        };
        return all;
    }


    public async Task<TEntity?> GenericReadById<TEntity>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>>? include) where TEntity : class
    {
        IQueryable<TEntity> query = Context.Set<TEntity>();

        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (include != null)
            query = query.Include(include);

        return await query.FirstOrDefaultAsync();
    }

    // filter and select used in get names
    public async Task<IEnumerable<TEntity>> GenericReadAll<TEntity>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TEntity>>? selectExpression, int? page, int? pageSize) where TEntity : class
    {
        IQueryable<TEntity> query = Context.Set<TEntity>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (page.HasValue && pageSize.HasValue)
        {
            GenericPagination(ref query, ref pageSize, ref page);
        }

        if (selectExpression != null)
            return await query.Select(selectExpression).ToListAsync();
        else
            return await query.ToListAsync();
    }


    // search
    public async Task<IEnumerable<TEntity>?> GenericSearchByText<TEntity>(Expression<Func<TEntity, bool>> filter, int? page, int? pageSize, Expression<Func<TEntity, TEntity>> selectExpression) where TEntity : class
    {
        try
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (page.HasValue && pageSize.HasValue)
            {
                int skip = (page.Value - 1) * pageSize.Value;
                query = query.Skip(skip).Take(pageSize.Value);
            }

            List<TEntity> results = await query.ToListAsync();
            return results;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<IEnumerable<TEntity>?> GenericSearchByText<TEntity>(int? parentId, Expression<Func<TEntity, bool>> filter1, Expression<Func<TEntity, bool>>? filter2, int? page, int? pageSize) where TEntity : class
    {
        try
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (filter1 != null)
            {
                query = query.Where(filter1);
            }

            if (parentId.HasValue && filter2 != null)
            {
                query = query.Where(filter2);
            }

            if (page.HasValue && pageSize.HasValue)
            {
                int skip = (page.Value - 1) * pageSize.Value;
                query = query.Skip(skip).Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }
        catch (Exception)
        {
            return null;
        }
    }
    
    public async Task<IEnumerable<TEntity>?> GenericSearchByText<TEntity>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>>? include, int? page, int? pageSize) where TEntity : class
    {
        try
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (include != null)
            {
                query = query.Include(include);
            }

            if (page.HasValue && pageSize.HasValue)
            {
                int skip = (page.Value - 1) * pageSize.Value;
                query = query.Skip(skip).Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }
        catch (Exception)
        {
            return null;
        }
    }




    #region protected methods
    protected static void GenericPagination<TQuery>(ref IQueryable<TQuery> query, ref int? pageSize, ref int? page, int totalCount = 0) where TQuery : class
    {

        if (page.HasValue && pageSize.HasValue)
        {
            //if (page.Value * pageSize > totalCount)
            //{

            //}
            int skip = (page.Value - 1) * pageSize.Value;
            query = query.Skip(skip).Take(pageSize.Value);
        }
        else
        {
            page = 0;
            pageSize = 0;
        }
    }




    //protected static void GenericStatus<TQuery>(ref IQueryable<TQuery> query) where TQuery : class
    //{

    //}
    #endregion






    #region private methods
    #endregion



















    // this func writed by chat
    //void test()
    //{

    //    //    try
    //    //    {
    //    //        var current = await Context.Set<TEntity>().SingleOrDefaultAsync(predicate);
    //    //        if (current != null)
    //    //        {
    //    //            var propertyInfo = (propertyExpression.Body as MemberExpression)?.Member as PropertyInfo;
    //    //            if (propertyInfo != null && propertyInfo.PropertyType == typeof(bool))
    //    //            {
    //    //                propertyInfo.SetValue(current, propertyValue);
    //    //                Context.Attach(entity);
    //    //                Context.Entry(entity).Property(propertyExpression).IsModified = true;
    //    //                var rowEffected = await Context.SaveChangesAsync();
    //    //                if (rowEffected > 0)
    //    //                    return new Response(true, $"Update on entity with Id: {id}");
    //    //            }
    //    //        }
    //    //        return new Response(false, $"Entity with Id: {id} not found");
    //    //    }
    //    //    catch (Exception)
    //    //    {
    //    //        return new Response(false, $"No changes on entity with Id: {id}");
    //    //    }
    //}


















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
