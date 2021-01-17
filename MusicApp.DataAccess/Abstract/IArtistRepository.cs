using MusicApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.DataAccess.Abstract
{
    public interface IArtistRepository:IBaseRepository<Artist>
    {
        Task<Artist> GetAlbums(int id);

        Task<Artist> GetLatestAlbums(int id, int takeCount = 10);
    }
}
