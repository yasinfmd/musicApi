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

        Task<BaseResponse<IEnumerable<AlbumDto>>> GetAlbumByArtist(int id);

        Task<BaseResponse<ArtistDto>> GetArtist(int id);
        Task<bool> isExists(Expression<Func<Albums, bool>> filter);
        Task<BaseResponse<string>> Delete(AlbumDto album);
        Task<BaseResponse<IEnumerable<AlbumDto>>> GetAll();
        Task<BaseResponse<AlbumDto>> GetByID(int id);

        Task<BaseResponse<Albums>> GetAlbumById(int id);

        Task<BaseResponse<AlbumUpdateDto>> Update(Albums album);

        Task<BaseResponse<string>> DeleteAlbumPhotos(DeleteAlbumPhotosModel deleteAlbumPhotosModel);

        //bak
        Task<BaseResponse<List<AlbumFilesDto>>> AddAlbumPhotos(AlbumArtistPhotosModel albumArtistPhotosModel);
        Task<BaseResponse<AlbumDto>> Insert(AlbumImagesModel album);

    }
}
