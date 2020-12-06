using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MusicApp.BaseEntity.Concrate;
namespace MusicApp.Entity
{
    public class AlbumsFiles:Base
    {
        [Key]
        public int AlbumId { get; set; }
        [Key]
        public int FileId { get; set; }
        public Albums Album { get; set; }
        public Files File { get; set; }


    }
}
