using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Business.Abstract;
using MusicApp.Entity;
using MusicApp.Logger.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class FilesController : ControllerBase
    {
        private readonly IFilesService _filesService;
        private readonly ILogService _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public FilesController(IFilesService filesService, ILogService logService, IMapper mapper, IWebHostEnvironment env)
        {
            _filesService = filesService;
            _logger = logService;
            _mapper = mapper;
            _env=env;
        }

        [HttpPost]
        [Route("[action]")]


        public async Task<IActionResult> store([FromForm]Files files)
        {
            var response = await _filesService.Insert(files);
            return Ok(response);
        }
    }
}
