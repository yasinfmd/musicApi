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
        private readonly MusicAppDbContext _context;
        public ArtistRepository(IBaseRepository<Artist> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public Task<int> CountAll()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountWhere(Expression<Func<Artist, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(Artist entityToDelete)
        {
            throw new NotImplementedException();
        }

        public Task<List<Artist>> Find(Expression<Func<Artist, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<Artist> FindOne(Expression<Func<Artist, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Artist>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Artist> GetByID(int id)
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
