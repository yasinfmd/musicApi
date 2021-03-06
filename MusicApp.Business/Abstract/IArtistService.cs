﻿using MusicApp.Dto;
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
        Task<bool> isExists(Expression<Func<Artist, bool>> filter);


        Task<BaseResponse<ArtistDto>> Insert(ArtistImageModel artist);

        Task<BaseResponse<ArtistDto>> Update(Artist artist);


        Task<BaseResponse<ArtistDto>> GetByID(int id);

        Task<BaseResponse<Artist>> GetArtist(int id);

        Task<BaseResponse<string>> Delete(ArtistDto artist);

        Task<BaseResponse<IEnumerable<ArtistDto>>> GetAll();

        Task<BaseResponse<FilesDto>> UpdateArtistProfileImage(UpdateProfilePhoto artistPhoto);

        Task<BaseResponse<IEnumerable<ArtistAlbumsDto>>> GetAlbums(int id);


        Task<BaseResponse<IEnumerable<ArtistAlbumsDto>>> GetLatest(int id ,int takeCount);


    }
}
