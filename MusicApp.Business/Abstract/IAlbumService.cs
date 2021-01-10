using MusicApp.Dto;
using MusicApp.Entity;
using MusicApp.Entity.ParameterModels;
using MusicApp.Entity.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Business.Abstract
{
    public interface IAlbumService
    {
        Task<BaseResponse<string>> Delete(AlbumDto album);
        Task<BaseResponse<IEnumerable<AlbumDto>>> GetAll();
        Task<bool> isExists(Expression<Func<Albums, bool>> filter);
        Task<BaseResponse<AlbumDto>> GetByID(int id);

        Task<BaseResponse<AlbumDto>> Insert(AlbumImagesModel album);

    }
}
