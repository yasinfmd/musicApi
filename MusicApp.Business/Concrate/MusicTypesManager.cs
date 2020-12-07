using MusicApp.Business.Abstract;
using MusicApp.DataAccess.Abstract;
using MusicApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Business.Concrate
{
    public class MusicTypesManager : IMusicTypesService
    {
        private readonly IMusicTypesRepository _musicTypesRepository;

        public MusicTypesManager(IMusicTypesRepository musicTypesRepository)
        {
            _musicTypesRepository = musicTypesRepository;
        }
        public Task<int> CountAll()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountWhere(Expression<Func<MusicTypes, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(MusicTypes entityToDelete)
        {
            throw new NotImplementedException();
        }

        public Task<List<MusicTypes>> Find(Expression<Func<MusicTypes, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<MusicTypes> FindOne(Expression<Func<MusicTypes, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MusicTypes>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<MusicTypes> GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<MusicTypes> Insert(MusicTypes musicTypes)
        {
            return await _musicTypesRepository.Insert(musicTypes);
        }

        public Task<MusicTypes> Update(MusicTypes entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
