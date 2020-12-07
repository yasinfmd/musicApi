using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Entity.ResponseModels
{
   public class ValidationErrorException
    {
        public List<ValidationErrorExceptionModel> Errors { get; set; } = new List<ValidationErrorExceptionModel>();
    }
}
