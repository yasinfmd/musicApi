using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MusicApp.Api.Hubs;
using MusicApp.Business.Abstract;
using MusicApp.Dto;
using MusicApp.Entity;
using MusicApp.Entity.ParameterModels;
using MusicApp.Entity.ResponseModels;
using MusicApp.Logger.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : MusicAppBaseController
    {
        private readonly IAlbumService _albumService;
        private readonly ILogService _logger;
        private readonly IArtistService _artistService;
        private readonly IHubContext<ArtistHub> _artistHub;
        public AlbumController(IAlbumService albumService,ILogService log,IArtistService artistService, IHubContext<ArtistHub> artistHub)
        {
            _albumService = albumService;
            _logger = log;
            _artistService = artistService;
            _artistHub = artistHub;
        }
        //AlbumArtistPhotosModel
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse<List<AlbumFilesDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> addAlbumPhotos([FromForm]AlbumArtistPhotosModel albumArtistPhotosModel)
        {
            try
            {
                _logger.LogInfo(ControllerContext.ActionDescriptor.DisplayName);
                if (ModelState.IsValid)
                {
                    var result = await _albumService.AddAlbumPhotos(albumArtistPhotosModel);
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception exception)
            {
                return ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }
        }

      

        [HttpGet]
        [Route("[action]/{artistId}")]
        public async Task<IActionResult> artist(int artistId, [FromQuery(Name = "includeAlbum")] bool includeAlbum)
        {
            try
            {

                var isExists = await _artistService.isExists(x => x.Id == artistId);
                if (!isExists)
                {
                    _logger.LogWarning($"{ControllerContext.ActionDescriptor.DisplayName} Not Found Artist Id : {artistId}");
                    return NotFound();
                }
                else
                {
                    if (artistId>0)
                    {
                        if (includeAlbum) {
                            _logger.LogInfo($"{ControllerContext.ActionDescriptor.DisplayName} Album Artist");
                            return Ok(await _albumService.GetAlbumByArtist(artistId));
                        }
                        else
                        {
                            _logger.LogInfo($"{ControllerContext.ActionDescriptor.DisplayName} Album Artist");
                            return Ok(await _albumService.GetArtist(artistId));
                        }
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            catch (Exception exception)
            {
                return ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }
        }


        [HttpPut]
        [Route("[action]/{albumId}")]
        public async Task<IActionResult> update (int albumId,Albums albums)
        {
            try
            {
                var isExists = await _albumService.isExists(x => x.Id == albumId);
                if (!isExists)
                {
                    _logger.LogWarning($"{ControllerContext.ActionDescriptor.DisplayName} Not Found Id : {albumId}");
                    return NotFound();
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var album = await _albumService.GetAlbumById(albumId);
                        album.Result.Desc = albums.Desc;
                        album.Result.Name = albums.Name;
                        album.Result.Year = albums.Year;
                        album.Result.ArtistId = albums.ArtistId;
                        var updatedAlbum = await _albumService.Update(album.Result);

                        return Ok(updatedAlbum);
                    }
                    else
                    {
                        return BadRequest();
                    }
              

                    //var album = await _artistService.GetByID(artistId);
                    //_logger.LogInfo($"{ControllerContext.ActionDescriptor.DisplayName} Finded Artist : {artist.Result}");
                    //return Ok(artist.Result);
                }
            }
            catch (Exception exception)
            {
                return ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> deleteAlbumPhotos(DeleteAlbumPhotosModel deleteAlbumPhotosModel)
        {
            try
            {
                _logger.LogInfo(ControllerContext.ActionDescriptor.DisplayName);
                if (ModelState.IsValid)
                {
                    var result = await _albumService.DeleteAlbumPhotos(deleteAlbumPhotosModel);
                    //await _musicTypesHub.Clients.All.SendAsync("newMusicTypeAdded",newMusicTypes);

                   _logger.LogInfo($"{ControllerContext.ActionDescriptor.DisplayName} Album Deleted Photos : {deleteAlbumPhotosModel.images}");
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception exception)
            {
                return ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }
        }


        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse<AlbumDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ValidationErrorExceptionModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> store([FromForm] AlbumImagesModel albumImagesModel)
        {
            try
            {
                _logger.LogInfo(ControllerContext.ActionDescriptor.DisplayName);
                if (ModelState.IsValid)
                {
                    var result = await _albumService.Insert(albumImagesModel);
                    await _artistHub.Clients.All.SendAsync("newAlbumAdded", new NewAlbumAddedHubModel {AlbumId=result.Result.Id,ArtistId=result.Result.Artist.Id } );
                    _logger.LogInfo($"{ControllerContext.ActionDescriptor.DisplayName} Album Created Name : {result.Result.Name}  Id : {result.Result.Id}");
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception exception)
            {
                return ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }
        }

        [HttpDelete]
        [Route("[action]/{albumId}")]
        [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> delete(int albumId)
        {
            try
            {
                _logger.LogInfo(ControllerContext.ActionDescriptor.DisplayName);
                var album = await _albumService.GetByID(albumId);
                if (album.Result == null)
                {

                    _logger.LogWarning($"{ControllerContext.ActionDescriptor.DisplayName} Not Found Id : {albumId}");
                    return NotFound();
                }
                else
                {

                    var deleted = await _albumService.Delete(album.Result);
                    _logger.LogInfo($"{ControllerContext.ActionDescriptor.DisplayName} Deleted Album: {albumId}");
                    return Ok(deleted);
                }
            }
            catch (Exception exception)
            {
                return ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }

        }


        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse<IList<AlbumDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> index()
        {
            try
            {
                _logger.LogInfo(ControllerContext.ActionDescriptor.DisplayName);
                var allAlbums = await _albumService.GetAll();
              _logger.LogInfo($"{ControllerContext.ActionDescriptor.DisplayName} Total Albums : {allAlbums.Result.Count()}");
                return Ok(allAlbums);
            }
            catch (Exception exception)
            {
                return ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }
        }
    }
}
