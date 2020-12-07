using MusicApp.Entity.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.DataAccess.Abstract
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {

        Task<int> Delete(TEntity entityToDelete);
        Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> filter);

        Task<IEnumerable<TEntity>> GetAll();


        Task<TEntity> GetByID(int id);

        Task<TEntity> Insert(TEntity entity);
        Task<TEntity> Update(TEntity entityToUpdate);


        Task<TEntity> FindOne(Expression<Func<TEntity, bool>> filter);

        Task<int> CountAll();
        Task<int> CountWhere(Expression<Func<TEntity, bool>> predicate);

    }
}
