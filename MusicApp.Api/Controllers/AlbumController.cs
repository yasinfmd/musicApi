using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Business.Abstract;
using MusicApp.Dto;
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

        public AlbumController(IAlbumService albumService,ILogService log)
        {
            _albumService = albumService;
            _logger = log;
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
                    //await _musicTypesHub.Clients.All.SendAsync("newMusicTypeAdded",newMusicTypes);

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
