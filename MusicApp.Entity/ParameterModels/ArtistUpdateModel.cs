using MusicApp.BaseEntity.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Entity.ParameterModels
{
   public class ArtistUpdateModel: Base
    {
        public string Name { get; set; }
        public string Info { get; set; }

        public int Gender { get; set; }
    }
}
