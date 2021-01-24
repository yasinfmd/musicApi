using MusicApp.Entity.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Business.Abstract
{
    public interface IUserService
    {
        Task<BaseResponse<string>> Register(UserRegisterModel userRegisterModel);
    }
}
