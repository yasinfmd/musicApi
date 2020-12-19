using Microsoft.EntityFrameworkCore;
using MusicApp.DataAccess.Abstract;
using MusicApp.Entity;
using MusicApp.Entity.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MusicApp.DataAccess.Concrate
{
    public class MusicTypesRepository : IMusicTypesRepository
    {

        private readonly IBaseRepository<MusicTypes> _baseRepository;
        private readonly MusicAppDbContext _context;

        public MusicTypesRepository(IBaseRepository<MusicTypes> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public async Task<int> CountAll()
        {

            return await _baseRepository.CountAll();
        }

        public async Task<int> CountWhere(Expression<Func<MusicTypes, bool>> expression)
        {
            return await _baseRepository.CountWhere(expression);
        }

        public async Task<int> Delete(MusicTypes musicTypes)
        {

            return await _baseRepository.Delete(musicTypes);
        }

        public async Task<int> DeleteById(int id)
        {
            return  await _baseRepository.DeleteById(id);
        }

        public async Task<List<MusicTypes>> Find(Expression<Func<MusicTypes, bool>> filter)
        {
            return await _baseRepository.Find(filter);
        }

        public async Task<MusicTypes> FindOne(Expression<Func<MusicTypes, bool>> filter)
        {
            return await _baseRepository.FindOne(filter);
        }

        public async Task<IEnumerable<MusicTypes>> GetAll()
        {
            return await _baseRepository.GetAll();
        }

        public async Task<MusicTypes> GetByID(int id)
        {
            return await _baseRepository.GetByID(id);
           
        }

        public async Task<IList<MusicTypes>> GetLast(Expression<Func<MusicTypes, int>> filter, int takeCount)
        {
            return await _baseRepository.GetLast(filter, takeCount);
        }

        public async Task<MusicTypes> Insert(MusicTypes musicTypes)
        {
            return await _baseRepository.Insert(musicTypes);
        }

        public async Task<bool> isExists(Expression<Func<MusicTypes, bool>> filter)
        {
            return await _baseRepository.isExists(filter);
        }

        public async Task<MusicTypes> Update(MusicTypes musicTypes)
        {
            return await _baseRepository.Update(musicTypes);
        }
    }
}