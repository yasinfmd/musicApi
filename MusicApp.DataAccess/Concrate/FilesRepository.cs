using MusicApp.DataAccess.Abstract;
using MusicApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.DataAccess.Concrate
{
    public class FilesRepository : IFilesRepository
    {
        private readonly IBaseRepository<Files> _baseRepository;
        private readonly MusicAppDbContext _context;

        public FilesRepository(IBaseRepository<Files> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public async Task<int> CountAll()
        {
            return await _baseRepository.CountAll();
        }

        public  async Task<int> CountWhere(Expression<Func<Files, bool>> filter)
        {
            return await _baseRepository.CountWhere(filter);
        }

        public async Task<int> Delete(Files files)
        {
            return await _baseRepository.Delete(files);
        }

        public async Task<int> DeleteById(int id)
        {
            return await _baseRepository.DeleteById(id);
        }

        public async Task<List<Files>> Find(Expression<Func<Files, bool>> filter)
        {
            return await _baseRepository.Find(filter);
        }

        public async Task<Files> FindOne(Expression<Func<Files, bool>> filter)
        {
            return await _baseRepository.FindOne(filter);
        }

        public async Task<IEnumerable<Files>> GetAll()
        {
            return await _baseRepository.GetAll();
        }

        public async Task<Files> GetByID(int id)
        {
            return await _baseRepository.GetByID(id);
        }

    

        public Task<IList<Files>> GetLast(Expression<Func<Files, int>> filter, int takeCount = 10)
        {
            throw new NotImplementedException();
        }

        public async Task<Files> Insert(Files files)
        {
            return await _baseRepository.Insert(files);
        }

        public async Task<bool> isExists(Expression<Func<Files, bool>> filter)
        {
            return await _baseRepository.isExists(filter);
        }

        public async Task<Files> Update(Files files)
        {
            return await _baseRepository.Update(files);
        }
    }
}
