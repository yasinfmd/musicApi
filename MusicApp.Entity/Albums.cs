using MusicApp.BaseEntity.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Entity
{
   public class Albums : Base
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Year { get; set; }
        public virtual ICollection<Music>? Musics { get; set; }
        public List<AlbumsFiles> AlbumsFiles { get; set; }
        public int? ArtistId { get; set; }
        public virtual Artist? Artist { get; set; }


    }
}
