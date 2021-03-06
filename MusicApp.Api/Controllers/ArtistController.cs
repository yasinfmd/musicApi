﻿using AutoMapper;
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
        public ArtistController(IArtistService artistService, ILogService logger) : base(logger)
        {
            _logger = logger;
            _artistService = artistService;
        }
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> updateArtistPhoto([FromForm] UpdateProfilePhoto updateProfilePhoto)
        {
            try
            {
                InfoLog(ControllerContext.ActionDescriptor.DisplayName);
                return Ok(await _artistService.UpdateArtistProfileImage(updateProfilePhoto));

            }
            catch (Exception exception)
            {
                return ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");

            }
        }


        [HttpGet]
        [Route("[action]/{artistId}")]
        public async Task<IActionResult> albums(int artistId, [FromQuery(Name = "takeCount")] int takeCount)
        {
            try
            {
                var isExists = await _artistService.isExists(x => x.Id == artistId);
                if (isExists)
                {
                    if (artistId > 0)
                    {
                        if (takeCount > 0)
                        {
                            return Ok(await _artistService.GetLatest(artistId,takeCount));
                        }
                        else
                        {
                            return Ok(await _artistService.GetAlbums(artistId));
                        }
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return CustomNotFound(artistId);
                }

            }
            catch (Exception exception)
            {
                return ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }
        }

        [HttpPut]
        [Route("[action]/{artistId}")]
        [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> update(int artistId, ArtistUpdateModel artistUpdateModel)
        {
            try
            {
                var isExists = await _artistService.isExists(x => x.Id == artistId);
                if (isExists)
                {

                    if (ModelState.IsValid)
                    {
                        var artist = await _artistService.GetArtist(artistId);
                        artist.Result.Name = artistUpdateModel.Name;
                        artist.Result.Info = artistUpdateModel.Info;
                        artist.Result.Gender = artistUpdateModel.Gender;
                        var updateArtist = await _artistService.Update(artist.Result);
                        InfoLog($"{ControllerContext.ActionDescriptor.DisplayName} ArtistUpdated Name : {updateArtist.Result.Name} and Id : {updateArtist.Result.Id}");
                        return Ok(updateArtist);
                    }
                    return BadRequest();
                }
                else
                {
                    return CustomNotFound(artistId);
                }
            }
            catch (Exception exception)
            {
                return ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");

            }
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
                    return CustomNotFound(artistId);
                }
                else
                {
                    var deleted = await _artistService.Delete(artist.Result);
                    InfoLog($"{ControllerContext.ActionDescriptor.DisplayName} Deleted Artist: {artistId}");
                    return Ok(deleted);
                }
            }
            catch (Exception exception)
            {
                return ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
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
                    return CustomNotFound(artistId);
                }
                else
                {
                    var artist = await _artistService.GetByID(artistId);
                    InfoLog($"{ControllerContext.ActionDescriptor.DisplayName} Finded Artist : {artist.Result}");
                    return Ok(artist.Result);
                }

            }
            catch (Exception exception)
            {
                return ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
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
                InfoLog(ControllerContext.ActionDescriptor.DisplayName);
                var allArtistList = await _artistService.GetAll();
                InfoLog($"{ControllerContext.ActionDescriptor.DisplayName} Total Artist : {allArtistList.Result.Count()}");
                return Ok(allArtistList);
            }
            catch (Exception exception)
            {
                return ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
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
                    InfoLog($"{ControllerContext.ActionDescriptor.DisplayName} ArtistCreated Name : {result.Result.Name}  Id : {result.Result.Id} Info : {result.Result.Info} and Gender : {result.Result.Gender}");
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception exception)
            {
                return ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }
        }
    }
}
