﻿using MusicApp.BaseEntity.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Dto
{
   public class AlbumFilesDto:Base
    {
        public string Path { get; set; }

        public bool isCover { get; set; }
    }
}
