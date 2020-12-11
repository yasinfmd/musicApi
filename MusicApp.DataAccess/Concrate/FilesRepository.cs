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
        public Task<int> CountAll()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountWhere(Expression<Func<Files, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(Files entityToDelete)
        {
            throw new NotImplementedException();
        }

        public Task<List<Files>> Find(Expression<Func<Files, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<Files> FindOne(Expression<Func<Files, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Files>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Files> GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Files> Insert(Files files)
        {
            return await _baseRepository.Insert(files);
        }

        public Task<bool> isExists(Expression<Func<Files, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<Files> Update(Files entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
