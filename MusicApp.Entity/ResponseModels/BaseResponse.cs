using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Entity.ResponseModels
{
    public class BaseResponse<T> where T : class 
    {
        public T Result { get; set; }
        public bool? isSuccess { get; set; }

        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
