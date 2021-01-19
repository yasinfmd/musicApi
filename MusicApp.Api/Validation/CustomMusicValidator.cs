using MusicApp.DataAccess.Abstract;
using MusicApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Validation
{
    public class CustomMusicValidator
    {
        private readonly IBaseRepository<Music> _baseRepository;
        public CustomMusicValidator(IBaseRepository<Music> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public bool UniqueMusicNameOfAlbum(Music music)
        {
            var musicResult = _baseRepository.FindOne(x => (x.Name.ToLower() == music.Name.ToLower() && x.AlbumId == music.AlbumId));
            if (musicResult.Result == null) return true;
               return false;
        
        }
    }
}
