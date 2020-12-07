using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Business.Abstract;
using MusicApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicTypesController : Controller
    {
        private readonly IMusicTypesService _musicTypesService;
        public MusicTypesController(IMusicTypesService musicTypesService)
        {
            _musicTypesService = musicTypesService;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> store([FromBody] MusicTypes musicTypes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result= await _musicTypesService.Insert(musicTypes);
            return Ok(result);
        }
    }
}
