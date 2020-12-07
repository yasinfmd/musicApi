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
        public Task<int> CountAll()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountWhere(Expression<Func<MusicTypes, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(MusicTypes entityToDelete)
        {
            throw new NotImplementedException();
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

        public async Task<BaseResponse<MusicTypesDto>> GetByID(int id)
        {
            BaseResponse<MusicTypesDto> baseResponse = new BaseResponse<MusicTypesDto>();
            var musicType = await _musicTypesRepository.GetByID(id);
            baseResponse.Result = _mapper.Map<MusicTypesDto>(musicType);
            return baseResponse;
        }

        public async Task<BaseResponse<MusicTypesDto>> Insert(MusicTypes musicTypes)
        {
            BaseResponse<MusicTypesDto> baseResponse = new BaseResponse<MusicTypesDto>();
            var newMusicTypes= await _musicTypesRepository.Insert(musicTypes);
            baseResponse.Result = _mapper.Map<MusicTypesDto>(newMusicTypes);
            return baseResponse;

        }

        public Task<MusicTypes> Update(MusicTypes entityToUpdate)
        {
            throw new NotImplementedException();
        }

 
    }
}