using Microsoft.AspNetCore.Http;
using MusicApp.BaseEntity.Concrate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MusicApp.Entity.ParameterModels
{
   public class ArtistImageModel:Base
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public int Gender { get; set; }
        [JsonIgnore]
        public IFormFile File { get; set; }
    }
}
