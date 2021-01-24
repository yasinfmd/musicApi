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

        protected void InfoLog(string log)
        {
            _logger.LogWarning(log);
        }

        protected void NotFoundLog(int id)
        {
            _logger.LogWarning($"{ControllerContext.ActionDescriptor.DisplayName} Not Found Id : {id}");
        }

        protected ObjectResult CustomNotFound(int id,string? message="Kayıt Bulunamadı")
        {
            NotFoundLog(id);
            return StatusCode(404, new NotFoundModel {Id=id,controller=ControllerContext.ActionDescriptor.ControllerName,method=ControllerContext.ActionDescriptor.ActionName,message=message });
        }


        protected ObjectResult ErrorInternal(Exception exception, string message)
        {
            _logger.LogError(exception, message);

            return StatusCode(500, new ErrorModel(exception.GetHashCode().ToString(), exception.Message));
        }
    }
}
