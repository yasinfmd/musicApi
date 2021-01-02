using AutoMapper;
using MusicApp.Business.Abstract;
using MusicApp.DataAccess.Abstract;
using MusicApp.Dto;
using MusicApp.Entity;
using MusicApp.Entity.ParameterModels;
using MusicApp.Entity.ResponseModels;
using System;
using System.Collections.Generic;
using System.IO;
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
        public async Task<BaseResponse<IEnumerable<ArtistDto>>> GetAll()
        {
          
            BaseResponse<IEnumerable<ArtistDto>> baseResponse = new BaseResponse<IEnumerable<ArtistDto>>();
            var allArtistList = await _artistRepository.GetAll();
            var mappedResult = _mapper.Map<IEnumerable<ArtistDto>>(allArtistList);
            baseResponse.Result = mappedResult;
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
            var mappedArtist = _mapper.Map<ArtistDto>(artist);
            baseResponse.Result = mappedArtist;
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

        public async Task<BaseResponse<FilesDto>> UpdateArtistProfileImage(UpdateProfilePhoto artistPhoto)
        {
            BaseResponse<FilesDto> baseResponse = new BaseResponse<FilesDto>();
            var artist = await GetByID(artistPhoto.Id);
            if (artist.Result != null)
            {
                var file = await _filesService.GetByID(artist.Result.File.Id);
                if (file.Result != null)
                {
                    string pathBuild = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\");
                    string[] fileName = artist.Result.File.Path.Split("Uploads/");
                    pathBuild = Path.Combine(pathBuild, fileName[fileName.Length - 1]);
                    var deletedFileFromStorage = _filesService.DeleteFile(pathBuild);
                    var newFile = await _filesService.UploadFileFromStorage(new Files { ImageFile = artistPhoto.File });
                    file.Result.Path = "http://localhost:5000/" + "Uploads/"+newFile;
                    file.Result.Name = artistPhoto.File.FileName;
                    file.Result.Size = Convert.ToInt32(artistPhoto.File.Length);
                    var updatedFile = await _filesService.Update(file.Result);
                    baseResponse.Result = updatedFile.Result;
                }
            }
            return baseResponse;
        }

        public async Task<BaseResponse<ArtistDto>> Update(Artist artist)
        {
            BaseResponse<ArtistDto> baseResponse = new BaseResponse<ArtistDto>();
            var updatedArtist = await _artistRepository.Update(artist);
            baseResponse.Result = new ArtistDto { Name = updatedArtist.Name, Gender = artist.Gender, Id = artist.Id, Info = artist.Info };
            return baseResponse;

            
        }

        public async Task<BaseResponse<Artist>> GetArtist(int id)
        {
            BaseResponse<Artist> baseResponse = new BaseResponse<Artist>();
            var artist = await _artistRepository.GetByID(id);
            baseResponse.Result = artist;
            return baseResponse;
        }
    }
}
