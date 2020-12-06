using System;
using System.Collections.Generic;
using System.Text;
using MusicApp.BaseEntity.Concrate;
namespace MusicApp.Entity
{
   public class Music:Base
    {
        public string Name{ get; set; }
        public string Minute { get; set; }
        public string Second { get; set; }


        public int AlbumId { get; set; }
        public virtual Albums Album{ get; set; }


        public int? MusicTypesId { get; set; }
        public virtual MusicTypes? MusicTypes { get; set; }

    }
}
