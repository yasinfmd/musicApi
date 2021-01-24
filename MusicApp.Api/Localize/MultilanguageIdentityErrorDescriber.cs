using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Localize
{
    public class MultilanguageIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateEmail),
                Description = string.Format("Email Zaten Mevcut", email)
            };
        }
        public override IdentityError DefaultError() { return new IdentityError { Code = nameof(DefaultError), Description = "Bilinmeyen Bir Hata Meydana Geldi" }; }
        public override IdentityError ConcurrencyFailure() { return new IdentityError { Code = nameof(ConcurrencyFailure), Description = "İyimser eşzamanlılık hatası, nesne değiştirildi." }; }
        public override IdentityError PasswordMismatch() { return new IdentityError { Code = nameof(PasswordMismatch), Description = "Yanlış parola." }; }
        public override IdentityError InvalidToken() { return new IdentityError { Code = nameof(InvalidToken), Description ="Geçersiz Token"}; }
        public override IdentityError LoginAlreadyAssociated() { return new IdentityError { Code = nameof(LoginAlreadyAssociated), Description = "Bu Girişe Sahip Zaten Bir Kullanıcı Var"}; }
        public override IdentityError InvalidUserName(string userName) { return new IdentityError { Code = nameof(InvalidUserName), Description = string.Format("Kullanıcı adı {0} geçersiz, yalnızca harf veya rakam içerebilir.", userName) }; }
        public override IdentityError InvalidEmail(string email) { return new IdentityError { Code = nameof(InvalidEmail), Description = string.Format("{0} e-postası geçersiz.", email) }; }
        public override IdentityError DuplicateUserName(string userName) { return new IdentityError { Code = nameof(DuplicateUserName), Description = string.Format("Kullanıcı Adı {0} zaten alınmış.", userName) }; }
        //public override IdentityError DuplicateEmail(string email) { return new IdentityError { Code = nameof(DuplicateEmail), Description = string.Format(_localizer["Email {0} is already taken."] , email) }; }
        public override IdentityError InvalidRoleName(string role) { return new IdentityError { Code = nameof(InvalidRoleName), Description = string.Format("Rol Adı Geçersiz Bak {0} ", role) }; }
        public override IdentityError DuplicateRoleName(string role) { return new IdentityError { Code = nameof(DuplicateRoleName), Description = string.Format("Rol adı {0} zaten alınmış.", role) }; }
        public override IdentityError UserAlreadyHasPassword() { return new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = "Kullanıcının zaten bir şifresi var." }; }
        public override IdentityError UserLockoutNotEnabled() { return new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = "Bu kullanıcı için kilitleme etkinleştirilmedi." }; }
        public override IdentityError UserAlreadyInRole(string role) { return new IdentityError { Code = nameof(UserAlreadyInRole), Description = string.Format("Kullanıcının Zaten Bu Rolu 1 {0}.", role) }; }
        public override IdentityError UserNotInRole(string role) { return new IdentityError { Code = nameof(UserNotInRole), Description = string.Format("Kullanıcı {0} rolünde değil.", role) }; }
        public override IdentityError PasswordTooShort(int length) { return new IdentityError { Code = nameof(PasswordTooShort), Description = string.Format("Şifreler en az {0} karakter olmalıdır.", length) }; }
        public override IdentityError PasswordRequiresNonAlphanumeric() { return new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "Parolalar en az bir alfasayısal olmayan karakter içermelidir." }; }
        public override IdentityError PasswordRequiresDigit() { return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "Şifreler en az bir rakam ('0' - '9') içermelidir." }; }
        public override IdentityError PasswordRequiresLower() { return new IdentityError { Code = nameof(PasswordRequiresLower), Description = "Parolalarda en az bir küçük harf ('a' - 'z') olmalıdır." }; }
        public override IdentityError PasswordRequiresUpper() { return new IdentityError { Code = nameof(PasswordRequiresUpper), Description = "Parolalarda en az bir büyük harf ('A' - 'Z') olmalıdır." }; }
            // DuplicateUserName, InvalidEmail, DuplicateUserName etc
        }
}
