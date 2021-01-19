using AutoMapper;
using AutoMapper.Configuration;
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
    public class MusicManager : IMusicService
    {
        private readonly IMusicRepository _musicRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public MusicManager(IMusicRepository musicRepository, IMapper mapper)
        {
            _mapper = mapper;
            _musicRepository = musicRepository;
        }

        public async Task<BaseResponse<IEnumerable<MusicDto>>> GetAll(bool includeMusicTypes,bool includeAlbums,bool isCoverPhoto=true)
        {
            BaseResponse<IEnumerable<MusicDto>> baseResponse = new BaseResponse<IEnumerable<MusicDto>>();
            var allMusics = await _musicRepository.GetAll(includeMusicTypes,includeAlbums,isCoverPhoto);
            baseResponse.Result = _mapper.Map<IEnumerable<MusicDto>>(allMusics);
            return baseResponse;

        }

        public async Task<BaseResponse<MusicDto>> Insert(Music music)
        {
            BaseResponse<MusicDto> baseResponse = new BaseResponse<MusicDto>();
           // var createdMusic = await _musicRepository.Insert(music);

            throw new NotImplementedException();
        }
    }
}
