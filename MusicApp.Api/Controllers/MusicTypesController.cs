using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Business.Abstract;
using MusicApp.Dto;
using MusicApp.Entity;
using MusicApp.Entity.ResponseModels;
using MusicApp.Logger.Abstract;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicTypesController : Controller
    {
        private readonly IMusicTypesService _musicTypesService;
        private readonly ILogService _logger;
        private readonly IMapper _mapper;

        public MusicTypesController(IMusicTypesService musicTypesService, ILogService logService,IMapper mapper)
        {
            _musicTypesService = musicTypesService;
            _logger = logService;
            _mapper = mapper;
        }

        [HttpPut]
        [Route("[action]/{musicTypeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> update(int musicTypeId,[FromBody] MusicTypes musicTypes)
        {
            try
            {
                _logger.LogInfo(ControllerContext.ActionDescriptor.DisplayName);
                var isExists = await _musicTypesService.isExists(x => x.Id == musicTypeId);
                if (!isExists)
                {
                    _logger.LogWarning($"{ControllerContext.ActionDescriptor.DisplayName} Not Found Id : {musicTypeId}");
                    return NotFound();
                }
                else
                {
                    var isNameUnique = await _musicTypesService.isExists(x => x.Name.ToLower() == musicTypes.Name.ToLower());
                    if (ModelState.IsValid && isNameUnique == false)
                    {
                        var musicType = await _musicTypesService.GetByID(musicTypeId);

                        musicType.Result.Name = musicTypes.Name;
                        var updateMusicTypes = await _musicTypesService.Update(musicType.Result);
                        _logger.LogInfo($"{ControllerContext.ActionDescriptor.DisplayName} MusicTypesCreated Name : {updateMusicTypes.Result.Name} and Id : {updateMusicTypes.Result.Id}");
                        return Ok(updateMusicTypes);
                    }
                    return BadRequest();

                }
            }
            catch (Exception exception)
            {
                return InternalError(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }

        }



        [HttpDelete]
        [Route("[action]/{musicTypeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> delete(int musicTypeId)
        {
            try
            {
                _logger.LogInfo(ControllerContext.ActionDescriptor.DisplayName);
                var isExists = await _musicTypesService.isExists(x => x.Id == musicTypeId);
                if (!isExists)
                {
                    _logger.LogWarning($"{ControllerContext.ActionDescriptor.DisplayName} Not Found Id : {musicTypeId}");
                    return NotFound();
                }
                else
                {
                    var musicType = await _musicTypesService.GetByID(musicTypeId);
                    _logger.LogInfo($"{ControllerContext.ActionDescriptor.DisplayName} Finded MusicTypes : {musicType}");
                    var deleted = await _musicTypesService.Delete(musicType.Result);
            
                    return Ok(deleted);
                }
            }
            catch(Exception exception)
            {
                return InternalError(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }

        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> store([FromBody] MusicTypes musicTypes)
        {
            try
            {
                _logger.LogInfo(ControllerContext.ActionDescriptor.DisplayName);
                var isExists = await _musicTypesService.isExists(x => x.Name.ToLower() == musicTypes.Name.ToLower());
                if (ModelState.IsValid && isExists==false)
                {
                    var newMusicTypes = await _musicTypesService.Insert(musicTypes);
                    _logger.LogInfo($"{ControllerContext.ActionDescriptor.DisplayName} MusicTypesCreated Name : {newMusicTypes.Result.Name} and Id : {newMusicTypes.Result.Id}");
                    return Created("Created", newMusicTypes);
                }
                return BadRequest();
            }
            catch (Exception exception)
            {
                return InternalError(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }
        }

        [HttpGet]
        [Route("[action]/{musicTypeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> show(int musicTypeId)
        {
            try
            {
                _logger.LogInfo(ControllerContext.ActionDescriptor.DisplayName);
                var isExists = await _musicTypesService.isExists(x=>x.Id== musicTypeId);
                if (!isExists)
                {
                    _logger.LogWarning($"{ControllerContext.ActionDescriptor.DisplayName} Not Found Id : {musicTypeId}");
                    return NotFound();
                }
                else
                {
                    var musicType = await _musicTypesService.GetByID(musicTypeId);
                    _logger.LogInfo($"{ControllerContext.ActionDescriptor.DisplayName} Finded MusicTypes : {musicType.Result}");
                    return Ok(musicType.Result);
                }

            }
            catch(Exception exception)
            {
                return InternalError(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");

            }
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> index()
        {
            try
            {
                _logger.LogInfo(ControllerContext.ActionDescriptor.DisplayName);
                var allMusicTypesList = await _musicTypesService.GetAll();
                _logger.LogInfo($"{ControllerContext.ActionDescriptor.DisplayName} Total MusicTypesCount : {allMusicTypesList.Result.Count()}");
                return Ok(allMusicTypesList);
            }
            catch (Exception exception)
            {
                return InternalError(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }
        }

        private ObjectResult InternalError(Exception exception, string message)
        {
            _logger.LogError(exception, message);
            return StatusCode(500, new ErrorModel(exception.GetHashCode().ToString(),exception.InnerException.Message));
        }
    }
}
