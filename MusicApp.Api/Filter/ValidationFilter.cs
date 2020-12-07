using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MusicApp.Logger.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Filter
{
    public class ValidationFilter : IActionFilter
    {
        private readonly ILogService _logger;
        public ValidationFilter(ILogService logger)
        {
            _logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

            _logger.LogWarning($"ModelState IsInvalid {context.ActionDescriptor.DisplayName} {context.ModelState.IsValid}");
            if (!context.ModelState.IsValid)
            {
                _logger.LogWarning($"ModelState IsInvalid {context.ActionDescriptor.DisplayName} {context.ModelState.IsValid}");
                context.Result = new BadRequestObjectResult("Test");
                    
                    //new BadRequestObjectResult(context.ModelState);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
