using AutoMapper;
using MusicApp.Business.Abstract;
using MusicApp.DataAccess.Abstract;
using MusicApp.Dto;
using MusicApp.Entity;
using MusicApp.Entity.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Business.Concrate
{
    public class AlbumManager : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IMapper _mapper;
        public AlbumManager(IAlbumRepository albumRepository, IMapper mapper)
        {
            _albumRepository = albumRepository;
            _mapper = mapper;
        }
   

       public async Task<BaseResponse<IEnumerable<AlbumDto>>> GetAll()
        {
            BaseResponse<IEnumerable<AlbumDto>> baseResponse = new BaseResponse<IEnumerable<AlbumDto>>();
            var result = await _albumRepository.GetAll();
            var mappedResult = _mapper.Map<IEnumerable<AlbumDto>>(result);
            baseResponse.Result = mappedResult;
            return baseResponse;
        }
    }
}
