using MusicApp.BaseEntity.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Entity.ParameterModels
{
    public class AlbumFilesModel:Base
    {
        public string Path { get; set; }
    }
    public class DeleteAlbumPhotosModel
    {
        public ICollection<AlbumFilesModel> images { get; set; }
    }
}
