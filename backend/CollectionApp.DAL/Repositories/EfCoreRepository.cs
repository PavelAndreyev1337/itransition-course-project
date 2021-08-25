using CollectionApp.DAL.DTO;
using CollectionApp.DAL.Enums;
using CollectionApp.DAL.Extensions;
using CollectionApp.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CollectionApp.DAL.Repositories
{
    public abstract class EfCoreRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntityWithId
        where TContext : DbContext
    {
        private readonly TContext _context;

        public EfCoreRepository(TContext context)
        {
            _context = context;
        }
        public TEntity Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            return entity;
        }

        public async Task<TEntity> Delete(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
            }
            return entity;
        }

        public IEnumerable<TEntity> Find(
            Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includes)
        {
            return _context.Set<TEntity>()
                .IncludeMultiple(includes)
                .Where(predicate)
                .ToList();
        }

        public async Task<TEntity> Get(
            int id,
            params Expression<Func<TEntity, object>>[] includes)
        {
            return await _context.Set<TEntity>()
                .IncludeMultiple(includes)
                .SingleOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<IEnumerable<TEntity>> GetAll(
            params Expression<Func<TEntity, object>>[] includes)
        {
            return await _context.Set<TEntity>()
                .IncludeMultiple(includes)
                .ToListAsync();
        }

        private IEnumerable<TEntity> Sort(
            IEnumerable<TEntity> entities,
            Sort sort = Enums.Sort.Asc,
            Func<TEntity, object> sortPredicate = null)
        {
            if (sort == Enums.Sort.Asc)
            {
                entities = entities.OrderBy(sortPredicate);
            }
            else if (sort == Enums.Sort.Desc)
            {
                entities = entities.OrderByDescending(sortPredicate);
            }
            return entities;
        }

        public EntityPageDTO<TEntity> Paginate(
            int pageSize = 10,
            int page = 1,
            Func<TEntity, bool> predicate = null,
            Sort sort = Enums.Sort.Asc,
            Func<TEntity, object> sortPredicate = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var dbSet = _context.Set<TEntity>();
            Func<TEntity, bool> defaultPredicate = entity => true;
            var count = dbSet.Where(predicate ?? defaultPredicate).Count();
            var entities = dbSet.IncludeMultiple(includes)
                .Where(predicate ?? defaultPredicate)
                .Reverse();
            if (sortPredicate != null)
            {
                entities = Sort(
                    entities,
                    sortPredicate: sortPredicate);
            }
            entities = entities.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return new EntityPageDTO<TEntity>
            {
                Page = new Page(count, page, pageSize),
                Entities = entities
            };
        }

        public TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
