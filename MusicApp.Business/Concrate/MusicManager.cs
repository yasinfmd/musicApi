using AutoMapper;
using AutoMapper.Configuration;
using MusicApp.Business.Abstract;
using MusicApp.DataAccess.Abstract;
using MusicApp.Dto;
using MusicApp.Entity;
using MusicApp.Entity.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        public async Task<BaseResponse<string>> Delete(Music music)
        {
            BaseResponse<string> baseResponse = new BaseResponse<string>();
            var isDeleted = await _musicRepository.DeleteById(music.Id);
            baseResponse.Result = isDeleted > 0 ? "Success Delete" : null;
            return baseResponse;
        }

        public async Task<BaseResponse<IEnumerable<MusicDto>>> GetAll(bool includeMusicTypes,bool includeAlbums,bool isCoverPhoto=true)
        {
            BaseResponse<IEnumerable<MusicDto>> baseResponse = new BaseResponse<IEnumerable<MusicDto>>();
            var allMusics = await _musicRepository.GetAll(includeMusicTypes,includeAlbums,isCoverPhoto);
            baseResponse.Result = _mapper.Map<IEnumerable<MusicDto>>(allMusics);
            return baseResponse;

        }

        public async Task<BaseResponse<MusicDto>> GetByID(int id, bool includeMusicTypes, bool includeAlbums, bool isCoverPhoto = true)
        {
            BaseResponse<MusicDto> baseResponse = new BaseResponse<MusicDto>();
            var music = await _musicRepository.GetByID(id, includeMusicTypes, includeAlbums, isCoverPhoto);
            var mappedResult = _mapper.Map<MusicDto>(music);
            baseResponse.Result = mappedResult;
            return baseResponse;
        }

        public async Task<BaseResponse<Music>> GetByID(int id)
        {
            BaseResponse<Music> baseResponse = new BaseResponse<Music>();
            var music = await _musicRepository.GetByID(id, false, false, false);
            baseResponse.Result = music;
            return baseResponse;
        }

        public async Task<BaseResponse<MusicDto>> Insert(Music music)
        {
            BaseResponse<MusicDto> baseResponse = new BaseResponse<MusicDto>();
            var createdMusic = await _musicRepository.Insert(music);
            var mappedResult = _mapper.Map<MusicDto>(createdMusic);
            baseResponse.Result = mappedResult;
            return baseResponse;

        }

        public async Task<bool> isExists(Expression<Func<Music, bool>> filter)
        {
            return await _musicRepository.isExists(filter);
        }

        public async Task<BaseResponse<MusicDto>> Update(Music music)
        {
            BaseResponse<MusicDto> baseResponse = new BaseResponse<MusicDto>();
            var updatedMusic = await _musicRepository.Update(music);
            var mappedResult = _mapper.Map<MusicDto>(updatedMusic);
            baseResponse.Result = mappedResult;
            return baseResponse;
        }
    }
}
