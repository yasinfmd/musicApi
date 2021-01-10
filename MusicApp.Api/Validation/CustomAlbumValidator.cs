using MusicApp.DataAccess.Abstract;
using MusicApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Validation
{
    public class CustomAlbumValidator
    {
        private readonly IBaseRepository<Albums> _baseRepository;

        public CustomAlbumValidator(IBaseRepository<Albums> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public bool UniqueName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var album = _baseRepository.FindOne(x => x.Name.ToLower() == name.ToLower());
                if (album.Result == null) return true;
                return false;

            }
            return false;

        }
    }
}
