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
    public interface IArtistService
    {
        Task<bool> isExists(ArtistImageModel artistImageModel);

        Task<BaseResponse<ArtistDto>> Insert(ArtistImageModel artist);

        Task<BaseResponse<Artist>> GetByID(int id);
        Task<BaseResponse<string>> Delete(Artist artist);


    }
}
