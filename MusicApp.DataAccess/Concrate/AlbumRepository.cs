using Microsoft.EntityFrameworkCore;
using MusicApp.DataAccess.Abstract;
using MusicApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.DataAccess.Concrate
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly IBaseRepository<Albums> _baseRepository;
        private MusicAppDbContext _context;

        public AlbumRepository(IBaseRepository<Albums> baseRepository, MusicAppDbContext context)
        {
            _baseRepository = baseRepository;
            _context = context;
        }



        public Task<int> CountAll()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountWhere(Expression<Func<Albums, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(Albums entityToDelete)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Albums>> Find(Expression<Func<Albums, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<Albums> FindOne(Expression<Func<Albums, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Albums>> GetAll()
        {
            return await _context.Albums.Include(x => x.Musics).ThenInclude(x=> x.MusicTypes).Include(x => x.Artist).Include(x => x.AlbumsFiles).ThenInclude(x => x.File).ToListAsync();
        
        }

        public Task<Albums> GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Albums>> GetLast(Expression<Func<Albums, int>> filter, int takeCount = 10)
        {
            throw new NotImplementedException();
        }

        public Task<Albums> Insert(Albums entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> isExists(Expression<Func<Albums, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<Albums> Update(Albums entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
