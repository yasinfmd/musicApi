using MusicApp.Dto;
using MusicApp.Entity;
using MusicApp.Entity.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Business.Abstract
{
   public interface IMusicService
    {
        Task<BaseResponse<MusicDto>> Insert(Music entity);
        Task<BaseResponse<string>> Delete(Music music);
        Task<BaseResponse<MusicDto>> GetByID(int id, bool includeMusicTypes, bool includeAlbums, bool isCoverPhoto = true);

        Task<BaseResponse<MusicDto>> Update(Music music);
        Task<BaseResponse<Music>> GetByID(int id);
        Task<bool> isExists(Expression<Func<Music, bool>> filter);
        Task<BaseResponse<IEnumerable<MusicDto>>> GetAll(bool includeMusicTypes,bool includeAlbums,bool isCoverPhoto=true);
    }
}
