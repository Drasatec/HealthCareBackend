﻿using DomainModel.Models;
using DomainModel.Models.Hospitals;
using System.Linq.Expressions;

namespace DomainModel.Interfaces
{
    public interface IGenericRepository// where T : class
    {
        //Task<T> Create(T entity);
        //Task<T?> ReadSingle(int id);
        //Task<T?> ReadSingleById(Expression<Func<T, bool>> expression, Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        //Task<IEnumerable<T>> Finde(Expression<Func<T, bool>> expression, int? take, int? skip);
        //Task<IEnumerable<T>> ReadAll(Expression<Func<T, bool>> expression, string[] include = null!);
        //Task<IEnumerable<T>> ReadAll(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        //Task<IEnumerable<T>> ReadAll(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int? page, int? pageSize, params Expression<Func<T, object>>[] includes);
       
        Task<Response<object>> GenericCreateWithImage<TEntity>(TEntity dto, Stream? image = null) where TEntity : class;
        Task<Response> GenericUpdate<TEntity>(List<TEntity> dto) where TEntity : class;
        Task<Response> GenericDelete<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> expression, params int[] ids) where TEntity : class;
        Task<Response> GenericUpdateSinglePropertyById<TEntity>(int id, TEntity entity, Expression<Func<TEntity, bool>> propertyExpression) where TEntity : class;
        Task<IEnumerable<TEntity>> GenericReadAll<TEntity>(Expression<Func<TEntity, bool>> filter, int? page, int? pageSize, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, params Expression<Func<TEntity, object>>[] includes) where TEntity : class;
        Task<IEnumerable<TEntity>> GenericReadAll<TEntity>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TEntity>> selectExpression, int? page, int? pageSize) where TEntity : class;
        Task<IEnumerable<TEntity>?> GenericSearchByText<TEntity>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TEntity>> selectExpression, int? page, int? pageSize) where TEntity : class;
        Task<IEnumerable<TEntity>?> GenericSearchByText<TEntity>(int? baseId, Expression<Func<TEntity, bool>> filter1, Expression<Func<TEntity, bool>>? filter2, int? page, int? pageSize) where TEntity : class;
    }
}
