using Microsoft.AspNetCore.Identity;
using MusicApp.Business.Abstract;
using MusicApp.Entity.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Business.Concrate
{
    public class UserServiceManager : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserServiceManager(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<BaseResponse<string>> Register(UserRegisterModel userRegisterModel)
        {
            BaseResponse<string> baseResponse = new BaseResponse<string>();
            var identityUser = new IdentityUser { Email = userRegisterModel.Email, UserName = userRegisterModel.Email };
            var result = await _userManager.CreateAsync(identityUser, userRegisterModel.Password);

            if (result.Succeeded)
            {
                baseResponse.Result = "Kullancı Kayıt Olma İşlemi Başarıyla Gerçekleşti";
                baseResponse.isSuccess = true;
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    var errorItem = new ErrorModel(item.Code, item.Description);
                    baseResponse.Errors.Add(errorItem);
                }
                baseResponse.isSuccess = false;
            }
            return baseResponse;
        }
    }
}
