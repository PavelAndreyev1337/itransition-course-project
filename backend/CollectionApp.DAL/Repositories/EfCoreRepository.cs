using CollectionApp.DAL.DTO;
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

        public async Task<EntityPageDTO<TEntity>> Paginate(int pageSize = 10,
            int page = 1,
            Func<TEntity, bool> predicate = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var dbSet = _context.Set<TEntity>();
            var count = await dbSet.CountAsync();
            Func<TEntity, bool> defaultPredicate = entity => true;
            var entities = _context.Set<TEntity>()
                .IncludeMultiple(includes)
                .Where(predicate ?? defaultPredicate)
                .Reverse()
                .Skip((page - 1) * pageSize)
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
