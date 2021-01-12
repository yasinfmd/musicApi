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
    public class AlbumManager : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IMapper _mapper;
        private readonly IFilesService _filesService;

        public AlbumManager(IAlbumRepository albumRepository, IMapper mapper,IFilesService filesService)
        {
            _albumRepository = albumRepository;
            _mapper = mapper;
            _filesService = filesService;
        }

        public  async Task<BaseResponse<List<AlbumFilesDto>>> AddAlbumPhotos(AlbumArtistPhotosModel albumArtistPhotosModel)
        {
            BaseResponse<List<AlbumFilesDto>> baseResponse = new BaseResponse<List<AlbumFilesDto>>();
            var album =await _albumRepository.FindOne(x => x.Id == albumArtistPhotosModel.AlbumId);
            List<AlbumsFiles> albumsFiles = new List<AlbumsFiles>();
            foreach (var item in albumArtistPhotosModel.Files)
            {
                Files f = new Files();
                string fileName = Path.GetFileNameWithoutExtension(item.Name);
                string extension = Path.GetExtension(item.FileName);
                fileName = fileName + "_" + Guid.NewGuid().ToString() + extension;
                string pathBuild = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\");
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\", fileName);
                using (var FileStream = new FileStream(path, FileMode.Create))
                {
                    await item.CopyToAsync(FileStream);
                }
                f.Name = item.FileName;
                f.Size = Convert.ToInt32(item.Length);
                f.Path = "http://localhost:5000/Uploads/" + fileName;
                albumsFiles.Add(new AlbumsFiles { Album = album, File = f });
            }
            album.AlbumsFiles = albumsFiles;
            var updatedAlbum = await _albumRepository.Update(album);
            var albumFileDto = _mapper.Map<List<AlbumFilesDto>>(updatedAlbum.AlbumsFiles);
            baseResponse.Result = albumFileDto;
            return baseResponse;
            // album
           // throw new NotImplementedException();
        }

        //bak
        public async Task<BaseResponse<string>> Delete(AlbumDto album)
        {
            BaseResponse<string> baseResponse = new BaseResponse<string>();

            var files = album.Images;
            if (files.Count>0)
            {
                string pathBuild = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\");
                foreach (var item in files)
                {
                    string[] fileName =item.Path.Split("Uploads/");
                    pathBuild = Path.Combine(pathBuild, fileName[fileName.Length - 1]);
                    var deletedFileFromStorage = _filesService.DeleteFile(pathBuild);
                    var deletedFile = await _filesService.Delete(new FilesDto { Id=item.Id,Path=item.Path});
                }
                var result = await _albumRepository.DeleteById(album.Id);
                baseResponse.Result = result > 0 ? "Success Delete" : null;
            }
            else
            {
                var result = await _albumRepository.DeleteById(album.Id);
                baseResponse.Result = result > 0 ? "Success Delete" : null;
            }
            return baseResponse;
        }

        public async Task<BaseResponse<string>> DeleteAlbumPhotos(DeleteAlbumPhotosModel deleteAlbumPhotosModel)
        {
            BaseResponse<string> baseResponse = new BaseResponse<string>();
            foreach (var item in deleteAlbumPhotosModel.images)
            {
                var filesDto = _mapper.Map<FilesDto>(item);
                await _filesService.Delete(filesDto);
            }
            baseResponse.Result = "Success Delete";
            return baseResponse;
        }


        public async Task<BaseResponse<IEnumerable<AlbumDto>>> GetAll()
        {
            BaseResponse<IEnumerable<AlbumDto>> baseResponse = new BaseResponse<IEnumerable<AlbumDto>>();
            var result = await _albumRepository.GetAll();
            var mappedResult = _mapper.Map<IEnumerable<AlbumDto>>(result);
            baseResponse.Result = mappedResult;
            return baseResponse;
        }

        public async Task<BaseResponse<AlbumDto>> GetByID(int id)
        {
            BaseResponse<AlbumDto> baseResponse = new BaseResponse<AlbumDto>();
            var result = await _albumRepository.GetByID(id);
            var mappedResult = _mapper.Map<AlbumDto>(result);
            baseResponse.Result = mappedResult;
            return baseResponse;

        }

        public async Task<BaseResponse<AlbumDto>> Insert(AlbumImagesModel album)
        {
            BaseResponse<AlbumDto> baseResponse = new BaseResponse<AlbumDto>();
            var albums = new Albums();
            albums.ArtistId = album.ArtistId;
            albums.Desc = album.Desc;
            albums.Name = album.Name;
            albums.Year = album.Year;
            var files = album.Files;
            List<AlbumsFiles> albumsFiles = new List<AlbumsFiles>();
            if (files !=null && files.Count > 0)
            {
                foreach (var item in files)
                {
                    Files f = new Files();
                    string fileName = Path.GetFileNameWithoutExtension(item.Name);
                    string extension = Path.GetExtension(item.FileName);
                    fileName = fileName + "_" + Guid.NewGuid().ToString() + extension;
                    string pathBuild = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\");
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\", fileName);
                    using (var FileStream = new FileStream(path, FileMode.Create))
                    {
                       await item.CopyToAsync(FileStream);
                    }
                    f.Name = item.FileName;
                    f.Size = Convert.ToInt32(item.Length);
                    f.Path = "http://localhost:5000/Uploads/" + fileName;
                    albumsFiles.Add(new AlbumsFiles { Album = albums, File = f });
                }
                albums.AlbumsFiles = albumsFiles;
            }
            var addedAlbum = await _albumRepository.Insert(albums);
            var result = await GetByID(addedAlbum.Id);
            baseResponse.Result = result.Result;
            return baseResponse;
        }

        public async Task<bool> isExists(Expression<Func<Albums, bool>> filter)
        {
            return await _albumRepository.isExists(filter);
        }
    }
}
