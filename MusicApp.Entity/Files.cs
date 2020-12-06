using System;
using System.Collections.Generic;
using System.Text;
using MusicApp.BaseEntity.Concrate;
namespace MusicApp.Entity
{
    public class Files:Base
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public int Size { get; set; }

        public List<AlbumsFiles> AlbumsFiles { get; set; }


    }
}
