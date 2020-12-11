using MusicApp.BaseEntity.Concrate;
using MusicApp.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Dto
{
    public class ArtistDto : Base
    {
        public string Name { get; set; }
        public string Info { get; set; }

        public int Gender { get; set; }
        public  FilesDto File {get;set;}
    }
}
