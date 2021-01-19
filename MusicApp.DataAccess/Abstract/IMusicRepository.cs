using MusicApp.Dto;
using MusicApp.Entity;
using MusicApp.Entity.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.DataAccess.Abstract
{
    public interface IMusicRepository : IBaseRepository<Music>
    {
        Task<IEnumerable<Music>> GetAll(bool includeMusicTypes,bool includeAlbums,bool isCoverPhoto=true);
    }
}
