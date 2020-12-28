using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MusicApp.Entity.ParameterModels
{
    public class UpdateProfilePhoto
    {
        public int Id { get; set; }
        [JsonIgnore]
        public IFormFile File { get; set; }
    }
}
