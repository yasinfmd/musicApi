using AutoMapper;
using MusicApp.Business.Abstract;
using MusicApp.DataAccess.Abstract;
using MusicApp.Dto;
using MusicApp.Entity;
using MusicApp.Entity.ParameterModels;
using MusicApp.Entity.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Business.Concrate
{
    public class ArtistManager : IArtistService
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IMapper _mapper;
        private readonly IFilesService _filesService;


        public ArtistManager(IArtistRepository artistRepository,IMapper mapper,IFilesService filesService)
        {
            _artistRepository = artistRepository;
            _mapper = mapper;
            _filesService = filesService;
        }

        public Task<BaseResponse<string>> Delete(Artist artist)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<Artist>> GetByID(int id)
        {
            BaseResponse<Artist> baseResponse = new BaseResponse<Artist>();
            var artist = await _artistRepository.GetByID(id);
            baseResponse.Result = artist;
            return baseResponse;
        }

        public async Task<BaseResponse<ArtistDto>> Insert(ArtistImageModel artistImageModel)
        {
            BaseResponse<ArtistDto> baseResponse = new BaseResponse<ArtistDto>();
            Files files = new Files
            {
                ImageFile = artistImageModel.File
            };
            var file = await _filesService.Insert(files);
            Artist artist = new Artist
            {
                FileId = file.Id,
                Gender = artistImageModel.Gender,
                Info = artistImageModel.Info,
                Name = artistImageModel.Name
            };
            var result = await _artistRepository.Insert(artist);
            var fileDto = new FilesDto { Id=file.Id,Path=file.Path};
            var artistDto = new ArtistDto { File = fileDto, Gender = result.Gender, Id = result.Id, Info = result.Info, Name = result.Name };
            baseResponse.Result = artistDto;
            return baseResponse;
        }

        public async Task<bool> isExists(ArtistImageModel artistImageModel)
        {
           // Artist artist = new Artist { Id = artistImageModel.Id, Gender = artistImageModel.Gender, Info = artistImageModel.Info, Name = artistImageModel.Name };
            return await _artistRepository.isExists(x=>x.Name.ToLower()== artistImageModel.Name.ToLower());
        }

    }
}
