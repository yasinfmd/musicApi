﻿using MusicApp.BaseEntity.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Dto
{
    public class MusicDto : Base
    {
        public string Name { get; set; }
        public string Minute { get; set; }
        public MusicTypesDto? MusicTypes { get; set; }

        public MusicAlbumDto? Album { get; set; }
    }
}
