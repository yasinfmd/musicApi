using MusicApp.BaseEntity.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.BaseEntity.Concrate
{
    public class Base : IBaseEntity
    {
        public int Id { get; set; }
    }
}
