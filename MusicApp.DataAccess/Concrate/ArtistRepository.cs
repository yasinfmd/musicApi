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
    public class ArtistRepository : IArtistRepository
    {
        private readonly IBaseRepository<Artist> _baseRepository;
        private MusicAppDbContext _context;

        public ArtistRepository(IBaseRepository<Artist> baseRepository,MusicAppDbContext context)
        {
            _baseRepository = baseRepository;
            _context = context;
        }
        public async Task<int> CountAll()
        {
            return await _baseRepository.CountAll();
        }

        public async Task<int> CountWhere(Expression<Func<Artist, bool>> filter)
        {
            return await _baseRepository.CountWhere(filter);
        }

        public async Task<int> Delete(Artist artist)
        {
            return await _baseRepository.Delete(artist);

        }

        public async Task<int> DeleteById(int id)
        {
            return await _baseRepository.DeleteById(id);
        }

        public Task<List<Artist>> Find(Expression<Func<Artist, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public  async Task<Artist> FindOne(Expression<Func<Artist, bool>> filter)
        {
            return await _baseRepository.FindOne(filter);
        }

        public async Task<IEnumerable<Artist>> GetAll()
        {
            return await _baseRepository.GetAll();
        }

        public async Task<Artist> GetByID(int id)
        {
            return await _context.Artist.Include(file => file.File).FirstOrDefaultAsync(x => x.Id == id);
        }


        public Task<IList<Artist>> GetLast(Expression<Func<Artist, int>> filter, int takeCount = 10)
        {
            throw new NotImplementedException();
        }

        public async Task<Artist> Insert(Artist artist)
        {
            return await _baseRepository.Insert(artist);
        }

        public async Task<bool> isExists(Expression<Func<Artist, bool>> filter)
        {
            return await _baseRepository.isExists(filter);
        }

        public Task<Artist> Update(Artist entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
