using MusicApp.Dto;
using MusicApp.Entity;
using MusicApp.Entity.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Business.Abstract
{
   public interface IMusicService
    {
        Task<BaseResponse<MusicDto>> Insert(Music entity);
        Task<BaseResponse<IEnumerable<MusicDto>>> GetAll(bool includeMusicTypes,bool includeAlbums,bool isCoverPhoto=true);
    }
}
