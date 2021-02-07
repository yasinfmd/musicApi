using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MusicApp.Business.Abstract;
using MusicApp.Entity.ParameterModels;
using MusicApp.Entity.ResponseModels;
using MusicApp.RabbitMQ;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        public async Task<AuthResponse<string>> Register(UserRegisterModel userRegisterModel)
        {
            AuthResponse<string> baseResponse = new AuthResponse<string>();
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

        public async Task<AuthResponse<string>> ConfirmEmail(string userId, string token)
        {
            AuthResponse<string> baseResponse = new AuthResponse<string>();
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
        public async Task<AuthResponse<string>> PasswordCheck(IdentityUser user, UserLoginModel userLoginModel)
        {
            AuthResponse<string> baseResponse = new AuthResponse<string>();
            var passwordCheck = await _userManager.CheckPasswordAsync(user, userLoginModel.Password);
            if (!passwordCheck)
            {
                baseResponse.Result = "Parola Yanlış";
                baseResponse.isSuccess = false;
                await _userManager.AccessFailedAsync(user);
                int failcount = await _userManager.GetAccessFailedCountAsync(user);
                if (failcount == 4)
                {
                    await _userManager.SetLockoutEnabledAsync(user, true);
                    await _userManager.SetLockoutEndDateAsync(user, new DateTimeOffset(DateTime.Now.AddMinutes(30))); //Eğer ki başarısız giriş denemesi 3'ü bulduysa ilgili kullanıcının hesabını kilitliyoruz.
                    baseResponse.Result = "Ard arda 5 başarısız giriş denemesi yaptığınızdan dolayı hesabınız 30 dakika kilitlenmiştir";
                    baseResponse.isSuccess = false;
                }
            }
            else
            {
                await _userManager.SetLockoutEnabledAsync(user, false);
                await _userManager.ResetAccessFailedCountAsync(user);
                var claims = new[]
                {
                        new Claim("Email",user.Email),
                        new Claim(ClaimTypes.NameIdentifier,user.Id),
                        new Claim(ClaimTypes.Name,user.UserName),
                        new Claim("LoginTime",DateTime.Now.ToString()),
                       
                };
                //new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
                var token = new JwtSecurityToken(issuer: "localhost:5000", audience: "localhost:5000", claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
                var stringTokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
                baseResponse.isSuccess = true;
                baseResponse.Result = stringTokenAsString;
                baseResponse.ExpireDate = token.ValidTo;
            }
            return baseResponse;
        }

        public async Task<AuthResponse<string>> Login(UserLoginModel userLoginModel)
        {
            AuthResponse<string> baseResponse = new AuthResponse<string>();
            var user = await _userManager.FindByEmailAsync(userLoginModel.Email);
            if (user == null)
            {
                baseResponse.Result = "Kullancı Bulunamadı";
                baseResponse.isSuccess = false;
            }
            var accountEnabled=  await _userManager.GetLockoutEnabledAsync(user);
            if (accountEnabled)
            {
                var lockDate = await _userManager.GetLockoutEndDateAsync(user);
                var nowDate = DateTime.Now;
                if (lockDate.Value.DateTime < nowDate)
                {
                    await _userManager.SetLockoutEnabledAsync(user, false);
                    await _userManager.ResetAccessFailedCountAsync(user);
                    baseResponse = await PasswordCheck(user, userLoginModel);
                }
                else
                {
                    baseResponse.isSuccess = false;
                    baseResponse.Result = $"Hesabınız : {lockDate.Value.DateTime}: Zamanına Kadar Kilitli";
                }

            }
            else
            {
               baseResponse= await PasswordCheck(user, userLoginModel);
            }
            return baseResponse;
        }
    }
}
