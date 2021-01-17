using MusicApp.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.DataAccess.Abstract
{
    public interface IAlbumRepository: IBaseRepository<Albums>
    {
        Task<IEnumerable<Albums>> GetAlbumByArtistId(int id);

        Task<Artist> GetArtist(int id);

    }
}
