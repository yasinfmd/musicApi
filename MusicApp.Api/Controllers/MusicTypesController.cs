using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Business.Abstract;
using MusicApp.Dto;
using MusicApp.Entity;
using MusicApp.Entity.ResponseModels;
using MusicApp.Logger.Abstract;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicTypesController : MusicAppBaseController
    {
        private readonly IMusicTypesService _musicTypesService;
        private readonly ILogService _logger;
        private readonly IMapper _mapper;

        public MusicTypesController(IMusicTypesService musicTypesService, ILogService logService,IMapper mapper):base(logService)
        {
            _musicTypesService = musicTypesService;
            _logger = logService;
            _mapper = mapper;
        }
        /// <summary>
        ///  Update MusicTypes ById
        /// </summary>
        /// <response code="200">Returns deleted message</response>
        /// <response code="400">If the item is null or if there is a  item name </response>   
        /// <response code="500">If there is a server side error</response>   
        /// <remarks>
        /// </remarks>
        /// <returns> </returns>
        [HttpPut]
        [Route("[action]/{musicTypeId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BaseResponse<MusicTypesDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ValidationErrorExceptionModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]

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
                return base.ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }

        }


        /// <summary>
        ///  Delete MusicTypes ById
        /// </summary>
        /// <response code="200">Returns deleted message</response>
        /// <response code="400">If the item is null or if there is a  item name </response>   
        /// <response code="500">If there is a server side error</response>   
        /// <remarks>
        /// </remarks>
        /// <returns> </returns>
        [HttpDelete]
        [Route("[action]/{musicTypeId}")]
        [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
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
                return base.ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }

        }
        /// <summary>
        ///  Create New MusicTypesItem
        /// </summary>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null or if there is a  item name </response>   
        /// <response code="500">If there is a server side error</response>   
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "name": "Jazz"
        ///     }
        ///
        /// </remarks>
        /// <returns> </returns>
        [HttpPost]
        [Route("[action]")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BaseResponse<MusicTypesDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<ValidationErrorExceptionModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
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
                return base.ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }
        }


        /// <summary>
        ///  Return  MusicTypes By Id
        /// </summary>
        /// <response code="200">Returns a single MusicType</response>
        /// <response code="404">Not Found By Id</response>
        /// <response code="500">If there is a server side error</response>   
        /// <returns> </returns>
        [HttpGet]
        [Route("[action]/{musicTypeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
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
                return base.ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");

            }
        }

        /// <summary>
        ///  Return All MusicTypes List BaseResponse
        /// </summary>
        /// <response code="200">Returns the all MusicTypes List</response>
        /// <response code="500">If there is a server side error</response>   
        /// <returns> </returns>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<MusicTypesDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
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
                return base.ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }
        }

    
    }
}
