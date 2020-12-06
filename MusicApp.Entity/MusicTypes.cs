using System;
using System.Collections.Generic;
using MusicApp.BaseEntity.Concrate;
namespace MusicApp.Entity
{
    public class MusicTypes:Base
    {
        public string Name { get; set; }
        public virtual ICollection<Music>? Music { get; set; }
    }
}
