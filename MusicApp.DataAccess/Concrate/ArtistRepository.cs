using Microsoft.EntityFrameworkCore;
using MusicApp.DataAccess.Abstract;
using MusicApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Artist>> Find(Expression<Func<Artist, bool>> filter)
        {
            return await _baseRepository.Find(filter);
        }

        public  async Task<Artist> FindOne(Expression<Func<Artist, bool>> filter)
        {
            return await _baseRepository.FindOne(filter);
        }

        public async Task<Artist> GetAlbums(int id)
        {
            //return await _context.Artist.Where(x => x.Id == id).Include(x => x.File)
              //  .Include(x => x.Albums).ThenInclude(x => x.Musics).ThenInclude(x => x.MusicTypes).Include(z => z.Albums).ThenInclude(y => y.AlbumsFiles).ThenInclude(y => y.File).FirstOrDefaultAsync();
    // await _context.Albums.Where(x => x.ArtistId == id).Include(x => x.Musics).ThenInclude(x => x.MusicTypes).Include(x => x.Artist ).ThenInclude(x => x.File).Include(y => y.AlbumsFiles).ThenInclude(y => y.File).ToListAsync();
            return await _context.Artist.Where(x => x.Id == id).Include(x => x.Albums).ThenInclude(x=>x.AlbumsFiles).ThenInclude(x=>x.File).FirstOrDefaultAsync();
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<Artist>> GetAll()
        {
            
            return await _context.Artist.Include(file => file.File).ToListAsync();
           // return await _baseRepository.GetAll();
        }

        public async Task<Artist> GetByID(int id)
        {
            return await _context.Artist.Include(file => file.File).FirstOrDefaultAsync(x => x.Id == id);
        }


        public Task<IList<Artist>> GetLast(Expression<Func<Artist, int>> filter, int takeCount = 10)
        {
            throw new NotImplementedException();
        }

        public async Task<Artist> GetLatestAlbums(int id , int takeCount = 10)
        {
            var result= await _context.Artist.Where(x=> x.Id == id).Include(x => x.Albums).ThenInclude(x=>x.AlbumsFiles).ThenInclude(x=>x.File).FirstOrDefaultAsync();
            result.Albums= result.Albums.OrderByDescending(x => x.Id).Take(takeCount);
            return result;
        }

        public async Task<Artist> Insert(Artist artist)
        {
            return await _baseRepository.Insert(artist);
        }

        public async Task<bool> isExists(Expression<Func<Artist, bool>> filter)
        {
            return await _baseRepository.isExists(filter);
        }

        public async Task<Artist> Update(Artist artist)
        {
            return await _baseRepository.Update(artist);
        }
    }
}
