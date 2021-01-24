using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Business.Abstract;
using MusicApp.Dto;
using MusicApp.Entity;
using MusicApp.Entity.ResponseModels;
using MusicApp.Logger.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MusicController : MusicAppBaseController
    {

        private readonly ILogService _logger;
        private readonly IMusicService _musicService; 

        public MusicController(IMusicService musicService, ILogService logger) : base(logger)
        {
            _logger = logger;
            _musicService = musicService;
        }


        [HttpPut]
        [Route("[action]/{musicId}")]
        [Produces("application/json")]
        public async Task<IActionResult> update(int musicId, [FromBody] Music music)
        {
            try
            {
                InfoLog(ControllerContext.ActionDescriptor.DisplayName);
                var isExists = await _musicService.isExists(x => x.Id == musicId);
                if (!isExists)
                {
                    return CustomNotFound(musicId);
                }
                else
                {
                    //kontrol edilecek album id ve muzik adına göre
                   // var isNameUnique = await _musicTypesService.isExists(x => x.Name.ToLower() == musicTypes.Name.ToLower());
                    if (ModelState.IsValid)
                    {
                        var findedMusic = await _musicService.GetByID(musicId);
                        //findedMusic.Result.Minute = music.Minute;
                        findedMusic.Result.Name = music.Name;
                        findedMusic.Result.Second = music.Second;
                        findedMusic.Result.Minute = music.Minute;
                        findedMusic.Result.AlbumId = music.AlbumId;
                        findedMusic.Result.MusicTypesId = music.MusicTypesId;
                        var updateMusic = await _musicService.Update(findedMusic.Result);
                        //   musicType.Result.Name = musicTypes.Name;
                      //  var updateMusicTypes = await _musicTypesService.Update(musicType.Result);
                       // InfoLog($"{ControllerContext.ActionDescriptor.DisplayName} MusicTypes Updated Name : {updateMusicTypes.Result.Name} and Id : {updateMusicTypes.Result.Id}");

                        return Ok(updateMusic);
                    }
                    return BadRequest();

                }
            }
            catch (Exception exception)
            {
                return ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }

        }

        [HttpPost]
        [Route("[action]")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BaseResponse<MusicTypesDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<ValidationErrorExceptionModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> store([FromBody] Music music)
        {
            try
            {
                InfoLog(ControllerContext.ActionDescriptor.DisplayName);
                // var isExists = await _musicTypesService.isExists(x => x.Name.ToLower() == musicTypes.Name.ToLower());
                if (ModelState.IsValid)
                {
                    var newMusic = await _musicService.Insert(music);
                    InfoLog($"{ControllerContext.ActionDescriptor.DisplayName} Music Name : {newMusic.Result.Name} and Id : {newMusic.Result.Id}");
                   // await _musicTypesHub.Clients.All.SendAsync("newMusicTypeAdded", newMusicTypes);
                    return Created("Created", newMusic);
                }
                return BadRequest();
            }
            catch (Exception exception)
            {
                return ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }
        }

        [HttpGet]
        [Route("[action]/{musicId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> show(int musicId, [FromQuery(Name = "includeMusicTypes")] bool includeMusicTypes = true, [FromQuery(Name = "includeAlbums")] bool includeAlbums = false, [FromQuery(Name = "isCoverPhoto")] bool isCoverPhoto = true)
        {
            try
            {
                _logger.LogInfo(ControllerContext.ActionDescriptor.DisplayName);
                var isExists = await _musicService.isExists(x => x.Id == musicId);
                if (!isExists)
                {
                    return CustomNotFound(musicId);
                }
                else
                {
                    var music = await _musicService.GetByID(musicId,includeMusicTypes,includeAlbums,isCoverPhoto);
                    InfoLog($"{ControllerContext.ActionDescriptor.DisplayName} Finded Music : {music.Result}");
                    return Ok(music);
                }

            }
            catch (Exception exception)
            {
                return ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");

            }
        }

        [HttpDelete]
        [Route("[action]/{musicId}")]
        [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> delete(int musicId)
        {
            try
            {
                _logger.LogInfo(ControllerContext.ActionDescriptor.DisplayName);
                var isExists = await _musicService.isExists(x => x.Id == musicId);
                if (!isExists)
                {
                    return CustomNotFound(musicId);
                }
                else
                {
                    var music = await _musicService.GetByID(musicId);
                    InfoLog($"{ControllerContext.ActionDescriptor.DisplayName} Finded MusicTypes : {music}");
                    return Ok(await _musicService.Delete(music.Result));
                }
            }
            catch (Exception exception)
            {
                return ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }

        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<MusicDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> index([FromQuery(Name = "includeMusicTypes")] bool includeMusicTypes=true, [FromQuery(Name = "includeAlbums")] bool includeAlbums = false, [FromQuery(Name = "isCoverPhoto")] bool isCoverPhoto = true)
        {
            try
            {
                _logger.LogInfo(ControllerContext.ActionDescriptor.DisplayName);
                var allMusicList = await _musicService.GetAll(includeMusicTypes, includeAlbums, isCoverPhoto);
                InfoLog($"{ControllerContext.ActionDescriptor.DisplayName} Total Music Count : {allMusicList.Result.Count()}");
                return Ok(allMusicList);
            }
            catch (Exception exception)
            {
                return ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }
        }

    }
}
