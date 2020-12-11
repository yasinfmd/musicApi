using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Business.Abstract;
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

        [HttpPost]
        [Route("[action]")]
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
