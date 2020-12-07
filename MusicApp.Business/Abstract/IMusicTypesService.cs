using MusicApp.DataAccess.Abstract;
using MusicApp.Dto;
using MusicApp.Entity;
using MusicApp.Entity.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Business.Abstract
{
    public interface IMusicTypesService 
    {
        Task<BaseResponse<MusicTypesDto>> GetByID(int id);



        Task<int> Delete(MusicTypes entityToDelete);
        Task<List<MusicTypes>> Find(Expression<Func<MusicTypes, bool>> filter);

        Task<BaseResponse<IEnumerable<MusicTypesDto>>> GetAll();



        Task<BaseResponse<MusicTypesDto>> Insert(MusicTypes entity);
        Task<MusicTypes> Update(MusicTypes entityToUpdate);


        Task<MusicTypes> FindOne(Expression<Func<MusicTypes, bool>> filter);

        Task<int> CountAll();
        Task<int> CountWhere(Expression<Func<MusicTypes, bool>> predicate);
    }
}
