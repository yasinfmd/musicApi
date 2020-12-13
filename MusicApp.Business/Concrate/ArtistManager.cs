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


        public ArtistManager(IArtistRepository artistRepository, IMapper mapper, IFilesService filesService)
        {
            _artistRepository = artistRepository;
            _mapper = mapper;
            _filesService = filesService;
        }
        public async Task<BaseResponse<IList<ArtistDto>>> GetAll()
        {
            List<ArtistDto> artistList = new List<ArtistDto>();
            BaseResponse<IList<ArtistDto>> baseResponse = new BaseResponse<IList<ArtistDto>>();
            var allArtistList = await _artistRepository.GetAll();
            foreach (var artist in allArtistList)
            {
                var file = await _filesService.GetByID((int)artist.FileId);
                var fileDto = new FilesDto { Id = artist.File.Id, Path = file.Result.Path };
                var artistDto = new ArtistDto
                {
                    File = fileDto,
                    Gender = artist.Gender,
                    Id = artist.Id,
                    Info = artist.Info,
                    Name = artist.Name
                };
                artistList.Add(artistDto);
            }
            baseResponse.Result = artistList;
            return baseResponse;

        }

        public async Task<BaseResponse<string>> Delete(ArtistDto artist)
        {
            BaseResponse<string> baseResponse = new BaseResponse<string>();
            await _filesService.Delete(artist.File);
            var isDeleted = await _artistRepository.DeleteById(artist.Id);
            baseResponse.Result = isDeleted > 0 ? "Success Delete" : null;
            return baseResponse;
        }

        public async Task<BaseResponse<ArtistDto>> GetByID(int id)
        {
            BaseResponse<ArtistDto> baseResponse = new BaseResponse<ArtistDto>();
            var artist = await _artistRepository.GetByID(id);
            var fileDto = new FilesDto { Id = artist.File.Id, Path = artist.File.Path };
            var artistDto = new ArtistDto
            {
                File = fileDto,
                Gender = artist.Gender,
                Id = artist.Id,
                Info = artist.Info,
                Name = artist.Name
            };
            baseResponse.Result = artistDto;
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
            var fileDto = new FilesDto { Id = file.Id, Path = file.Path };
            var artistDto = new ArtistDto { File = fileDto, Gender = result.Gender, Id = result.Id, Info = result.Info, Name = result.Name };
            baseResponse.Result = artistDto;
            return baseResponse;
        }

        public async Task<bool> isExists(Expression<Func<Artist, bool>> filter)
        {
            return await _artistRepository.isExists(filter);
        }
    }
}
