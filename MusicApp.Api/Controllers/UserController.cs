using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Business.Abstract;
using MusicApp.Entity.ResponseModels;
using MusicApp.Logger.Abstract;
using MusicApp.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : MusicAppBaseController
    {
        private readonly IUserService _userService;
        private readonly ILogService _logger;

        public UserController(IUserService userService, ILogService logger) : base(logger)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        [Route("[action]")]

        public async Task<IActionResult> confirmEmail([FromQuery(Name = "userId")] string userId, [FromQuery(Name = "token")] string token)
        {
            if(string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            {
                return NotFound();
            }

            var result = await _userService.ConfirmEmail(userId, token);
            if(result.isSuccess !=null && result.isSuccess == true)
            {
                return Ok(result);
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> register([FromBody] UserRegisterModel userRegisterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    InfoLog(ControllerContext.ActionDescriptor.DisplayName);
                    var result = await _userService.Register(userRegisterModel);
                    if (result.isSuccess !=null && result.isSuccess == true)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return Ok(result);
                        //login error
                    }
                       //return Ok(await _artistService.UpdateArtistProfileImage(updateProfilePhoto));


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
