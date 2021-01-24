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
    public class MusicRepository : IMusicRepository
    {
        private readonly IBaseRepository<Music> _baseRepository;
        private readonly MusicAppDbContext _context;
        public MusicRepository(IBaseRepository<Music> baseRepository, MusicAppDbContext context)
        {
            _context = context;
            _baseRepository = baseRepository;
        }
        public async Task<int> CountAll()
        {
            return await _baseRepository.CountAll();
        }

        public async Task<int> CountWhere(Expression<Func<Music, bool>> filter)
        {
            return await _baseRepository.CountWhere(filter);
        }

        public async Task<int> Delete(Music entity)
        {
            return await _baseRepository.Delete(entity);
        }

        public async Task<int> DeleteById(int id)
        {
            return await _baseRepository.DeleteById(id);
        }

        public async Task<List<Music>> Find(Expression<Func<Music, bool>> filter)
        {
            return await _baseRepository.Find(filter);
        }

        public async Task<Music> FindOne(Expression<Func<Music, bool>> filter)
        {
            return await _baseRepository.FindOne(filter);
        }

        //public async Task<IEnumerable<Music>> GetAll(List<string> includeArray)
        //{
        //    string expression = "MusicTypes;Album;Album.AlbumsFiles;Album.AlbumsFiles.File";
        //    string[] includes = expression.Split(';');

        //    if (includeArray.Count > 0)
        //    {

        //    int i = 0;
        //    foreach (string include in includes)
        //    {
        //        ++i;
        //        IQueryable<Music> result=  _context.Musics.Include(include);
        //        if (i == includes.Length)
        //        {
        //            return await result. ToListAsync();
        //        }
        //    }
        //    }

        //        return await _context.Musics.ToListAsync();


        //}

        public async Task<IEnumerable<Music>> GetAll()
        {
            return await _context.Musics.ToListAsync();
        }

        public async Task<IEnumerable<Music>> GetAll(bool includeMusicTypes, bool includeAlbums,bool isCoverPhoto=true)
        {
            if (includeAlbums == true && includeAlbums == true)
            {
                //bakılacak
                if (isCoverPhoto)
                {
                    //Where(af=>af.isCover == false)

                    //var result = await _context.Musics.Include(x => x.MusicTypes).Include(x => x.Album).Include(x=>x.Album.AlbumsFiles).ThenInclude(af => af.AlbumsFiles).ThenInclude(z => z.File).ToListAsync();
                    
                    var result = await _context.Musics.Include(x => x.MusicTypes).Include(x => x.Album).ThenInclude(t=>t.AlbumsFiles).ThenInclude(z => z.File).ToListAsync();
                    foreach (var item in result)
                    {
                        item.Album.AlbumsFiles = item.Album.AlbumsFiles.Where(x => x.isCover == true).ToList();
                    }
                    //result[0].Album.AlbumsFiles = result[0].Album.AlbumsFiles.Where(t => t.isCover == true).ToList() ;
                    return result;
                }
                else
                {
                    return await _context.Musics.Include(x => x.MusicTypes).Include(x => x.Album).ThenInclude(y => y.AlbumsFiles).ThenInclude(z => z.File).ToListAsync();
                }
            }
            else if (includeAlbums == true)
            {
                return await _context.Musics.Include(x => x.Album).ThenInclude(y => y.AlbumsFiles).ThenInclude(z => z.File).ToListAsync();
            }
            else if (includeMusicTypes == true)
            {
                return await _context.Musics.Include(x => x.MusicTypes).ToListAsync();
            }
            else
            {
                return await _context.Musics.ToListAsync();
            }
        }

        public async Task<Music> GetByID(int id)
        {
            return await _baseRepository.GetByID(id);
        }

        public async Task<Music> GetByID(int id, bool includeMusicTypes, bool includeAlbums, bool isCoverPhoto = true)
        {
            if (includeAlbums == true && includeAlbums == true)
            {
                //bakılacak
                if (isCoverPhoto)
                {
                    //Where(af=>af.isCover == false)

                    //var result = await _context.Musics.Include(x => x.MusicTypes).Include(x => x.Album).Include(x=>x.Album.AlbumsFiles).ThenInclude(af => af.AlbumsFiles).ThenInclude(z => z.File).ToListAsync();

                    var result = await _context.Musics.Include(x => x.MusicTypes).Include(x => x.Album).ThenInclude(t => t.AlbumsFiles).ThenInclude(z => z.File).FirstOrDefaultAsync(x=>x.Id == id);
                    result.Album.AlbumsFiles = result.Album.AlbumsFiles.Where(x => x.isCover == true).ToList();
                    return result;
                }
                else
                {
                    return await _context.Musics.Include(x => x.MusicTypes).Include(x => x.Album).ThenInclude(y => y.AlbumsFiles).ThenInclude(z => z.File).FirstOrDefaultAsync(x=>x.Id == id);
                }
            }
            else if (includeAlbums == true)
            {
                return await _context.Musics.Include(x => x.Album).ThenInclude(y => y.AlbumsFiles).ThenInclude(z => z.File).FirstOrDefaultAsync(x => x.Id == id);
            }
            else if (includeMusicTypes == true)
            {
                return await _context.Musics.Include(x=>x.MusicTypes).FirstOrDefaultAsync(x => x.Id == id);
            }
            else
            {
                return await _context.Musics.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<IList<Music>> GetLast(Expression<Func<Music, int>> filter, int takeCount = 10)
        {
            return await _baseRepository.GetLast(filter, takeCount);
        }

        public async Task<Music> Insert(Music entity)
        {
            return await _baseRepository.Insert(entity);
        }

        public async Task<bool> isExists(Expression<Func<Music, bool>> filter)
        {
            return await _baseRepository.isExists(filter);
        }

        public async Task<Music> Update(Music entityToUpdate)
        {
            return await _baseRepository.Update(entityToUpdate);
        }
    }
}
