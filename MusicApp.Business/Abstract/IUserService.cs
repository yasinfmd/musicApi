using MusicApp.Entity.ParameterModels;
using MusicApp.Entity.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Business.Abstract
{
    public interface IUserService
    {
        Task<AuthResponse<string>> Login(UserLoginModel userLoginModel);

        Task<AuthResponse<string>> Register(UserRegisterModel userRegisterModel);

        Task<AuthResponse<string>> ConfirmEmail(string userId,string token);

        Task<AuthResponse<string>> ForgotPassword(string email);

        Task<AuthResponse<string>> UpdateNewPassword(NewPasswordModel newPasswordModel);

    }
}
