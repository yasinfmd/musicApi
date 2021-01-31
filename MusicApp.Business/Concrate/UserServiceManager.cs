using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using MusicApp.Business.Abstract;
using MusicApp.Entity.ParameterModels;
using MusicApp.Entity.ResponseModels;
using MusicApp.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MusicApp.Business.Concrate
{
    public class UserServiceManager : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private static Publisher _publisher;
        private readonly IConfiguration _configuration;


        public UserServiceManager(UserManager<IdentityUser> userManager,IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public async Task<BaseResponse<string>> Register(UserRegisterModel userRegisterModel)
        {
            BaseResponse<string> baseResponse = new BaseResponse<string>();
            var identityUser = new IdentityUser { Email = userRegisterModel.Email, UserName = userRegisterModel.Email };
            var result = await _userManager.CreateAsync(identityUser, userRegisterModel.Password);

            if (result.Succeeded)
            {
                var emailConfirm = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
                var encodedEmailToken = Encoding.UTF8.GetBytes(emailConfirm);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);
                string uri = $"{_configuration["Server"]}api/user/confirmEmail?userId={identityUser.Id}&token={validEmailToken}";
                var jsonData = JsonSerializer.Serialize(new UserEmailModel() {Email=userRegisterModel.Email,Message=$"<h1>Email Adresi Onaylama</h1> <p>Lütfen Email Onaylayın <a href='{uri}'> Tıklayın</a> </p>"});
                _publisher = new Publisher("mailQueque", jsonData);
                _publisher = new Publisher("test", jsonData);


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

        public async Task<BaseResponse<string>> ConfirmEmail(string userId, string token)
        {
            BaseResponse<string> baseResponse = new BaseResponse<string>();
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                baseResponse.Result = "Kullancı Bulunamadı";
                baseResponse.isSuccess = false;
            }
            else
            {
                var decodedToken = WebEncoders.Base64UrlDecode(token);
                string normalToken= Encoding.UTF8.GetString(decodedToken);
                var result = await _userManager.ConfirmEmailAsync(user, normalToken);
                if (result.Succeeded)
                {
                    baseResponse.Result = "Kullancı Kayıt Doğrulama İşlemi Başarıyla Gerçekleşti";
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
            }

            return baseResponse;
        }
    }
}
