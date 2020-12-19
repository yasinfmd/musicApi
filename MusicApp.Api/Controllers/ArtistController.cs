using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

namespace MusicApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : MusicAppBaseController
    {
        private readonly IArtistService _artistService;
        private readonly ILogService _logger;
        private readonly IMapper _mapper;
        private readonly IFilesService _filesService;
        public ArtistController(IArtistService artistService, ILogService logger, IMapper mapper, IFilesService filesService):base(logger)
        {
            _logger = logger;
            _artistService = artistService;
            _mapper = mapper;
            _filesService = filesService;
        }

        [HttpDelete]
        [Route("[action]/{artistId}")]
        [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> delete(int artistId)
        {
            try
            {
                _logger.LogInfo(ControllerContext.ActionDescriptor.DisplayName);
                var artist = await _artistService.GetByID(artistId);
                if (artist.Result == null)
                {
                     
                    _logger.LogWarning($"{ControllerContext.ActionDescriptor.DisplayName} Not Found Id : {artistId}");
                    return NotFound();
                }
                else
                {
                    var deleted = await _artistService.Delete(artist.Result);
                    _logger.LogInfo($"{ControllerContext.ActionDescriptor.DisplayName} Deleted Artist: {artistId}");
                    return Ok(deleted);
                }
            }
            catch (Exception exception)
            {
                return base.ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }

        }

        [HttpGet]
        [Route("[action]/{artistId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> show(int artistId)
        {
            try
            {
                _logger.LogInfo(ControllerContext.ActionDescriptor.DisplayName);
                var isExists = await _artistService.isExists(x => x.Id == artistId);
                if (!isExists)
                {
                    _logger.LogWarning($"{ControllerContext.ActionDescriptor.DisplayName} Not Found Id : {artistId}");
                    return NotFound();
                }
                else
                {
                    var artist = await _artistService.GetByID(artistId);
                    _logger.LogInfo($"{ControllerContext.ActionDescriptor.DisplayName} Finded Artist : {artist.Result}");
                    return Ok(artist.Result);
                }

            }
            catch (Exception exception)
            {
                return base.ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse<IList<ArtistDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> index()
        {
            try
            {
                _logger.LogInfo(ControllerContext.ActionDescriptor.DisplayName);
                var allArtistList = await _artistService.GetAll();
                _logger.LogInfo($"{ControllerContext.ActionDescriptor.DisplayName} Total Artist : {allArtistList.Result.Count()}");
                return Ok(allArtistList);
            }
            catch (Exception exception)
            {
                return base.ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse<ArtistDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ValidationErrorExceptionModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> store([FromForm] ArtistImageModel artistImageModel)
        {
            try
            {
                _logger.LogInfo(ControllerContext.ActionDescriptor.DisplayName);
                if (ModelState.IsValid)
                {
                        var result = await _artistService.Insert(artistImageModel);
                    //await _musicTypesHub.Clients.All.SendAsync("newMusicTypeAdded",newMusicTypes);
                    
                    _logger.LogInfo($"{ControllerContext.ActionDescriptor.DisplayName} ArtistCreated Name : {result.Result.Name}  Id : {result.Result.Id} Info : {result.Result.Info} and Gender : {result.Result.Gender}");
                        return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception exception)
            {
               // base.ErrorInternal(exception, exception.Message);
                return base.ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }
        }
    }
}
