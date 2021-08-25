using CollectionApp.DAL.DTO;
using CollectionApp.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CollectionApp.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll(
            params Expression<Func<TEntity, object>>[] includes);
        EntityPageDTO<TEntity> Paginate(
            int pageSize = 10,
            int page = 1,
            Func<TEntity, bool> predicate = null,
            Sort sort = Sort.Asc,
            Func<TEntity, object> sortPredicate = null,
            params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> Get(int id, params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> Find(
            Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includes);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        Task<TEntity> Delete(int id);
    }
}
