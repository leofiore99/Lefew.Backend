using Lefew.Shared.Entities;
using Meritus.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lefew.Repositories.Base
{
    public abstract class Repository<TEntity, TContext> : IRepository<TEntity>, IDisposable
        where TEntity : Entity
        where TContext : DbContext
    {
        protected TContext Db;
        protected DbSet<TEntity> DbSet;

        protected Repository(TContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual Task<TEntity> GetById(int id)
        {
            return DbSet.FirstOrDefaultAsync(c => c.Id == id);
        }
        public virtual Task<List<TEntity>> GetAllBy(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            var results = await DbSet.ToListAsync();

            return results;
        }

        public virtual Task Insert(TEntity entity)
        {
            DbSet.Add(entity);
            return Db.SaveChangesAsync();
        }

        public virtual async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await Db.SaveChangesAsync();
        }

        public virtual Task Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            return Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
