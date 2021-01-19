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
    public class MusicController : MusicAppBaseController
    {

        private readonly ILogService _logger;
        private readonly IMusicService _musicService; 

        public MusicController(IMusicService musicService, ILogService logger) : base(logger)
        {
            _logger = logger;
            _musicService = musicService;
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
                _logger.LogInfo(ControllerContext.ActionDescriptor.DisplayName);
                // var isExists = await _musicTypesService.isExists(x => x.Name.ToLower() == musicTypes.Name.ToLower());
                if (ModelState.IsValid)
                {
                    var newMusic = await _musicService.Insert(music);
                    _logger.LogInfo($"{ControllerContext.ActionDescriptor.DisplayName} Music Name : {newMusic.Result.Name} and Id : {newMusic.Result.Id}");
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
        [Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<MusicDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> index([FromQuery(Name = "includeMusicTypes")] bool includeMusicTypes=true, [FromQuery(Name = "includeAlbums")] bool includeAlbums = false, [FromQuery(Name = "isCoverPhoto")] bool isCoverPhoto = true)
        {
            try
            {
                _logger.LogInfo(ControllerContext.ActionDescriptor.DisplayName);
                var allMusicList = await _musicService.GetAll(includeMusicTypes, includeAlbums, isCoverPhoto);
                _logger.LogInfo($"{ControllerContext.ActionDescriptor.DisplayName} Total Music Count : {allMusicList.Result.Count()}");
                return Ok(allMusicList);
            }
            catch (Exception exception)
            {
                return ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }
        }

    }
}
