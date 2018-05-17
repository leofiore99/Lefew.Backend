using Lefew.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Meritus.Shared.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> GetById(int id);

        Task<List<TEntity>> GetAllBy(Expression<Func<TEntity, bool>> predicate);

        Task<List<TEntity>> GetAll();

        Task Update(TEntity entity);

        Task Insert(TEntity entity);

        Task Delete(TEntity entity);
    }
}
