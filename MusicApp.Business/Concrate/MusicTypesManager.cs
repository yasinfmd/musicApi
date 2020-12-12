using AutoMapper;
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
    public class MusicTypesManager : IMusicTypesService
    {
        private readonly IMusicTypesRepository _musicTypesRepository;
        private readonly IMapper _mapper;

        public MusicTypesManager(IMusicTypesRepository musicTypesRepository,IMapper mapper)
        {
            _musicTypesRepository = musicTypesRepository;
            _mapper = mapper;
        }
        public async Task<BaseResponse<string>> CountAll()
        {
            BaseResponse<string> baseResponse = new BaseResponse<string>();
            int count = await _musicTypesRepository.CountAll();
            baseResponse.Result = count.ToString();
            return baseResponse;
        }

        public async Task<BaseResponse<string>> CountWhere(Expression<Func<MusicTypes, bool>> filter)
        {
            BaseResponse<string> baseResponse = new BaseResponse<string>();
            int count = await _musicTypesRepository.CountWhere(filter);
            baseResponse.Result = count.ToString();
            return baseResponse;
        }

        public async Task<BaseResponse<string>> Delete(MusicTypes musicTypes)
        {
            BaseResponse<string> baseResponse = new BaseResponse<string>();
            var isDeleted = await _musicTypesRepository.Delete(musicTypes);
            baseResponse.Result = isDeleted > 0 ? "Success Delete" : null;
            return baseResponse;
        }

        public Task<List<MusicTypes>> Find(Expression<Func<MusicTypes, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<MusicTypes> FindOne(Expression<Func<MusicTypes, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<IEnumerable<MusicTypesDto>>> GetAll()
        {
            BaseResponse<IEnumerable<MusicTypesDto>> baseResponse = new BaseResponse<IEnumerable<MusicTypesDto>>();
            var allMusicTypesList = await _musicTypesRepository.GetAll();
            baseResponse.Result = _mapper.Map<IEnumerable<MusicTypesDto>>(allMusicTypesList);
            return baseResponse;

        }

        public async Task<BaseResponse<MusicTypes>> GetByID(int id)
        {
            BaseResponse<MusicTypes> baseResponse = new BaseResponse<MusicTypes>();
            var musicType = await _musicTypesRepository.GetByID(id);
            baseResponse.Result = musicType;
                //_mapper.Map<MusicTypesDto>(musicType);
            return baseResponse;
        }

        public async Task<MusicTypes> getid(int id)
        {
            return await _musicTypesRepository.GetByID(id);
        }

        public async Task<BaseResponse<MusicTypesDto>> Insert(MusicTypes musicTypes)
        {
            BaseResponse<MusicTypesDto> baseResponse = new BaseResponse<MusicTypesDto>();
            var newMusicTypes= await _musicTypesRepository.Insert(musicTypes);
            baseResponse.Result = _mapper.Map<MusicTypesDto>(newMusicTypes);
            return baseResponse;

        }

        public async Task<bool> isExists(Expression<Func<MusicTypes, bool>> filter)
        {
            return await _musicTypesRepository.isExists(filter);
        }

        public async Task<BaseResponse<MusicTypesDto>> Update(MusicTypes musicTypes)
        {
            BaseResponse<MusicTypesDto> baseResponse = new BaseResponse<MusicTypesDto>();
            var updatedMusicTypes = await _musicTypesRepository.Update(musicTypes);
            baseResponse.Result = _mapper.Map<MusicTypesDto>(updatedMusicTypes);
            return baseResponse;
        }

 
    }
}