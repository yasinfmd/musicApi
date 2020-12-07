using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Entity.ResponseModels
{
   public class ValidationErrorExceptionModel
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }
}
