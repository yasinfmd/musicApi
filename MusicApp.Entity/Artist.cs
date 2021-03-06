﻿using System;
using System.Collections.Generic;
using System.Text;
using MusicApp.BaseEntity.Concrate;

namespace MusicApp.Entity
{
    public class Artist:Base
    {
        public string Name { get; set; }
        public string Info { get; set; }

        public int Gender { get; set; }

        public virtual IEnumerable<Albums>? Albums { get; set; }

        public int? FileId { get; set; }
        public  Files? File { get; set; }


    }
}
