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
        Task<MusicTypes> getid(int id);
        Task<BaseResponse<MusicTypes>> GetByID(int id);

        Task<bool> isExists(Expression<Func<MusicTypes, bool>> filter);
        Task<BaseResponse<string>> Delete(MusicTypes musicTypes);
        Task<List<MusicTypes>> Find(Expression<Func<MusicTypes, bool>> filter);

        Task<BaseResponse<IEnumerable<MusicTypesDto>>> GetAll();



        Task<BaseResponse<MusicTypesDto>> Insert(MusicTypes entity);
        Task<BaseResponse<MusicTypesDto>> Update(MusicTypes musicTypes);


        Task<MusicTypes> FindOne(Expression<Func<MusicTypes, bool>> filter);

        Task<BaseResponse<string>> CountAll();
        Task<int> CountWhere(Expression<Func<MusicTypes, bool>> predicate);
    }
}
