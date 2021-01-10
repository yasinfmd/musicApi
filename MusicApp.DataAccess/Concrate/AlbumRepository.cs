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



        public async Task<int> CountAll()
        {
            return await _baseRepository.CountAll();
        }

        public async Task<int> CountWhere(Expression<Func<Albums, bool>> predicate)
        {
            return await _baseRepository.CountWhere(predicate);
        }

        public async Task<int> Delete(Albums entityToDelete)
        {
            return await _baseRepository.Delete(entityToDelete);
        }

        public async Task<int> DeleteById(int id)
        {
            return await _baseRepository.DeleteById(id);
        }

        public async Task<List<Albums>> Find(Expression<Func<Albums, bool>> filter)
        {
            return await _baseRepository.Find(filter);
        }

        public async Task<Albums> FindOne(Expression<Func<Albums, bool>> filter)
        {
            return await _baseRepository.FindOne(filter);
        }

        public async Task<IEnumerable<Albums>> GetAll()
        {
            return await _context.Albums.Include(x => x.Musics).ThenInclude(x=> x.MusicTypes).Include(x => x.Artist).Include(x => x.AlbumsFiles).ThenInclude(x => x.File).ToListAsync();
        
        }

        public async Task<Albums> GetByID(int id)
        {
            return await _context.Albums.Include(x => x.Musics).ThenInclude(x => x.MusicTypes).Include(x => x.Artist).ThenInclude(x=>x.File).Include(y => y.AlbumsFiles).ThenInclude(y=>y.File).FirstOrDefaultAsync(x=>x.Id==id);
        }

        public Task<IList<Albums>> GetLast(Expression<Func<Albums, int>> filter, int takeCount = 10)
        {
            throw new NotImplementedException();
        }

        public async Task<Albums> Insert(Albums entity)
        {
            return await _baseRepository.Insert(entity);
        }

        public async Task<bool> isExists(Expression<Func<Albums, bool>> filter)
        {
            return await _baseRepository.isExists(filter);
        }

        public Task<Albums> Update(Albums entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
