using MusicApp.DataAccess;
using MusicApp.DataAccess.Abstract;
using MusicApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Validation
{
    public class CustomArtistValidation
    {
        private readonly IBaseRepository<Artist> _baseRepository;

        public CustomArtistValidation(IBaseRepository<Artist> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public bool UniqueName(string name)
        {
            var artist = _baseRepository.FindOne(x => x.Name.ToLower() == name.ToLower());

            if (artist == null) return true;
            return false;
        }
    }
}
