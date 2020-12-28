using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Business.Abstract;
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
        private readonly IMapper _mapper;
        private readonly IFilesService _filesService;

        public AlbumController(IAlbumService albumService,ILogService log)
        {
            _albumService = albumService;
            _logger = log;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> index()
        {
            try
            {
                _logger.LogInfo(ControllerContext.ActionDescriptor.DisplayName);
                var allMusicTypesList = await _albumService.GetAll();
              //  _logger.LogInfo($"{ControllerContext.ActionDescriptor.DisplayName} Total MusicTypesCount : {allMusicTypesList.Result.Count()}");
                return Ok(allMusicTypesList);
            }
            catch (Exception exception)
            {
                return ErrorInternal(exception, $"{ControllerContext.ActionDescriptor.DisplayName} Exception Message : {exception.Message} - {exception.InnerException}");
            }
        }
    }
}
