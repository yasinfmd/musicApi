using MusicApp.BaseEntity.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Dto
{
    public class MusicAlbumDto:Base
    {
        public string Desc { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public List<AlbumFilesDto> Images { get; set; }
    }
}
