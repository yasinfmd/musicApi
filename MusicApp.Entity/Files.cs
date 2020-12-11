using System;
using System.Collections.Generic;
using System.Text;
using MusicApp.BaseEntity.Concrate;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace MusicApp.Entity
{
    public class Files:Base
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public int Size { get; set; }

        public List<AlbumsFiles> AlbumsFiles { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        [JsonIgnore]
        public IFormFile ImageFile { get; set; }

    }
}
