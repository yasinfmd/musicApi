using Microsoft.EntityFrameworkCore;
using MusicApp.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MusicApp.DataAccess.Concrate
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        private MusicAppDbContext context;
        private DbSet<T> dbSet;
        public BaseRepository(MusicAppDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        public Task<int> CountAll()
        {
            return dbSet.CountAsync();
        }

        public Task<int> CountWhere(Expression<Func<T, bool>> filter)
        {
            return dbSet.CountAsync(filter);
        }

        public async Task<int> Delete(T entity)
        {
            dbSet.Remove(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteById(int id)
        {
            var item= await dbSet.FindAsync(id);
            dbSet.Remove(item);
            return await context.SaveChangesAsync();
        }

        public async Task<List<T>> Find(Expression<Func<T, bool>> filter)
        {
            return await dbSet.Where(filter).ToListAsync();
        }

        public async Task<T> FindOne(Expression<Func<T, bool>> filter)
        {
            return await dbSet.SingleOrDefaultAsync(filter);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T> GetByID(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<T> Insert(T entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> isExists(Expression<Func<T, bool>> filter)
        {
            return  await dbSet.AnyAsync(filter);
           
        }

        public async Task<T> Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }


    }
}
