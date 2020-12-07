using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MusicApp.Entity.ResponseModels;
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

            if (!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState
                 .Where(x => x.Value.Errors.Count > 0)
                 .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();
                var errorResponse = new ValidationErrorException();
                foreach (var error in errorsInModelState)
                {
                    foreach (var subError in error.Value)
                    {
                        _logger.LogWarning($"ModelState IsInvalid {context.ActionDescriptor.DisplayName} Error Key : {error.Key} , ErrorValue: {subError}");
                        var errorModel = new ValidationErrorExceptionModel()
                        {
                            FieldName = error.Key,
                            Message = subError
                        };
                        errorResponse.Errors.Add(errorModel);

                    }
                    context.Result = new BadRequestObjectResult(errorResponse);
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
