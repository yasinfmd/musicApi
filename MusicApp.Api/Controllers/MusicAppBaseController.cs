using Microsoft.AspNetCore.Mvc;
using MusicApp.Entity.ResponseModels;
using MusicApp.Logger.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Controllers
{
    public class MusicAppBaseController:ControllerBase
    {
        private readonly ILogService _logger;
        public MusicAppBaseController()
        {
        }
        public MusicAppBaseController(ILogService log)
        {
            _logger = log;
        }

        protected ObjectResult ErrorInternal(Exception exception, string message)
        {
            _logger.LogError(exception, message);

            return StatusCode(500, new ErrorModel(exception.GetHashCode().ToString(), exception.Message));
        }
    }
}
