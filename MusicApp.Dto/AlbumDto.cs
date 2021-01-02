using MusicApp.BaseEntity.Concrate;
using MusicApp.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Dto
{
    public class AlbumDto : Base
    {
        public string Desc { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public ArtistDto Artist { get; set; }
        public List<AlbumFilesDto> Images { get; set; }

        public List<MusicDto> Musics { get; set; }
    }
}
