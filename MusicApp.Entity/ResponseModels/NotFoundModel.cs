using MusicApp.BaseEntity.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Entity.ResponseModels
{
    public class NotFoundModel:Base
    {
        public string? message { get; set; }
        public string? controller { get; set; }

        public string? method { get; set; }
    }
}
