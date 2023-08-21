using DomainModel.Models;
using DomainModel.Models.Hospitals;
using System.Linq.Expressions;

namespace DomainModel.Interfaces
{
    public interface IGenericRepository
    {

        Task<Response<object>> GenericCreateWithImage<TEntity>(TEntity dto, Stream? image = null) where TEntity : class;
        Task<Response> GenericUpdate<TEntity>(List<TEntity> dto) where TEntity : class;
        Task<Response> GenericUpdateSinglePropertyById<TEntity>(int id, TEntity entity, Expression<Func<TEntity, object>> propertyExpression) where TEntity : class;
        Task<IEnumerable<TEntity>?> GenericSearchByText<TEntity>(Expression<Func<TEntity, bool>> filter, int? page, int? pageSize, Expression<Func<TEntity, TEntity>> selectExpression) where TEntity : class;
        Task<IEnumerable<TEntity>?> GenericSearchByText<TEntity>(int? parentId, Expression<Func<TEntity, bool>> filter1, Expression<Func<TEntity, bool>>? filter2, int? page, int? pageSize) where TEntity : class;
        Task<Response<TEntity>> GenericCreate<TEntity>(TEntity entity) where TEntity : class;
        Task<TEntity?> GenericReadById<TEntity>(Expression<Func<TEntity, bool>> filter,Expression<Func<TEntity, object>>? include= null) where TEntity : class;
        Task<IEnumerable<TEntity>> GenericReadAll<TEntity>(Expression<Func<TEntity, bool>>? filter, Expression<Func<TEntity, TEntity>>? selectExpression, int? page, int? pageSize) where TEntity : class;
        
        
        
        
        
        Task<IEnumerable<TEntity>?> GenericSearchByText<TEntity>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>>? include, int? page, int? pageSize) where TEntity : class;
        Task<Response> GenericUpdate<TEntity>(TEntity entity, params Expression<Func<TEntity, object>>[]? propertiesNotModified) where TEntity : class;
        Task<Response> GenericDelete<TEntity>(Expression<Func<TEntity, bool>> expression, params int[] ids) where TEntity : class;
        Task<PagedResponse<TEntity>?> GenericReadAllWihInclude<TEntity>(Expression<Func<TEntity, bool>>? filter, Expression<Func<TEntity, object>>? orderBy, Expression<Func<TEntity, object>>? include, int? page, int? pageSize) where TEntity : class;
        Task<TResult?> GenericReadSingle<TEntity, TResult>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult?>>? selectExpression)
            where TEntity : class
            where TResult : class;
        Task<IEnumerable<TResult>> GenericSelectionReadAll<TEntity, TResult>(Expression<Func<TEntity, bool>>? filter, Expression<Func<TEntity, TResult>> selectExpression, Expression<Func<TEntity, object>>? order, int? page, int? pageSize) where TEntity : class;
        Task<Response> GenericCreateRange<TEntity>(List<TEntity> entity) where TEntity : class;
        Task<int?> GenericCount<TEntity>(Expression<Func<TEntity, bool>>? filter = null) where TEntity : class;
        Task<Response> UpdateSinglePropertyInEntities<TEntity>(Expression<Func<TEntity, bool>> filter, Action<TEntity> updateAction) where TEntity : class;
        Task<Response> GenericUpdatePropertiesById<TEntity>(int id, TEntity entity, params Expression<Func<TEntity, object>>[]? propertiesIsModified) where TEntity : class;
    }
}
