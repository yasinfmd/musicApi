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
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly ILogService _logger;
        private readonly IMapper _mapper;
        private readonly IFilesService _filesService;
        public ArtistController(IArtistService artistService, ILogService logger, IMapper mapper, IFilesService filesService)
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
                if (artist == null)
                {
                     
                    _logger.LogWarning($"{ControllerContext.ActionDescriptor.DisplayName} Not Found Id : {artistId}");
                    return NotFound();
                }
                else
                {
                    var deleted = await _artistService.Delete(artist.Result);

                    return Ok(deleted);
                    //var musicType = await _musicTypesService.GetByID(musicTypeId);
                    //_logger.LogInfo($"{ControllerContext.ActionDescriptor.DisplayName} Finded MusicTypes : {musicType}");

                 //   return Ok(deleted);
                }
            }
            catch (Exception exception)
            {
                return InternalError(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
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
                var isExists = await _artistService.isExists(artistImageModel);
                if (ModelState.IsValid)
                {
                    if (isExists == true)
                    {
                        _logger.LogWarning($"{ControllerContext.ActionDescriptor.DisplayName} Record Not Unique  Name: {artistImageModel.Name}");
                    }
                    else
                    {
                        var result = await _artistService.Insert(artistImageModel);
                        _logger.LogInfo($"{ControllerContext.ActionDescriptor.DisplayName} ArtistCreated Name : {result.Result.Name}  Id : {result.Result.Id} Info : {result.Result.Info} and Gender : {result.Result.Gender}");

                        return Ok(result);
                    }
                }
                return BadRequest();
            }
            catch (Exception exception)
            {
                return InternalError(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }
        }

        private ObjectResult InternalError(Exception exception, string message)
        {
            _logger.LogError(exception, message);
            return StatusCode(500, new ErrorModel(exception.GetHashCode().ToString(), exception.InnerException.Message));
        }
    }
}
