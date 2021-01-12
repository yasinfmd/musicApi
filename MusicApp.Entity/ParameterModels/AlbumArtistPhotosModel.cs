using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MusicApp.Entity.ParameterModels
{
    public class AlbumArtistPhotosModel
    {
        public int AlbumId { get; set; }

        [JsonIgnore]
        public List<IFormFile> Files { get; set; }
    }
}
