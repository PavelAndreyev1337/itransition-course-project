using CollectionApp.DAL.DTO;
using CollectionApp.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionApp.DAL.Repositories
{
    public abstract class EfCoreRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class
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

        public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).ToList();
        }

        public async Task<TEntity> Get(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<EntityPageDTO<TEntity>> Paginate(int pageSize = 10,
            int page = 1,
            Func<TEntity, bool> predicate = null)
        {
            var dbSet = _context.Set<TEntity>();
            var count = await dbSet.CountAsync();
            Func<TEntity, bool> defaultPredicate = entity => true;
            var entities = _context.Set<TEntity>()
                .Where(predicate ?? defaultPredicate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return new EntityPageDTO<TEntity>
            {
                Page = new Page(count, page, page),
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
