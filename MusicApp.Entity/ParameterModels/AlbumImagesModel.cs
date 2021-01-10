using Microsoft.AspNetCore.Http;
using MusicApp.BaseEntity.Concrate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MusicApp.Entity.ParameterModels
{
   public class AlbumImagesModel:Base
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Year { get; set; }

        [JsonIgnore]
        public List<IFormFile>? Files { get; set; }

        public int ArtistId { get; set; }
    }
}
