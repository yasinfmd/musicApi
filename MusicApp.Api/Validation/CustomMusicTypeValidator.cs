using MusicApp.DataAccess.Abstract;
using MusicApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Validation
{
    public class CustomMusicTypeValidator
    {
        private readonly IBaseRepository<MusicTypes> _baseRepository;

        public CustomMusicTypeValidator(IBaseRepository<MusicTypes> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public bool UniqueName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var mtypes = _baseRepository.FindOne(x => x.Name.ToLower() == name.ToLower());
                if (mtypes.Result == null) return true;
                return false;

            }
            return false;

        }
    }
}
