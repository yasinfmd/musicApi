using FluentValidation;
using MusicApp.Entity.ParameterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Validation
{
    public class NewPasswordValidator : AbstractValidator<NewPasswordModel>
    {
        public NewPasswordValidator()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("Email Adı Boş Olamaz")
                .EmailAddress().WithMessage("Lütfen Geçerli Bir Email Adresi Giriniz")
                .NotEmpty().WithMessage("Email  Adı Boş Olamaz");
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("Parola Boş Olamaz").MinimumLength(8).WithMessage("Minimumu 8 Karakter Olmalıdır");
            RuleFor(x => x.Token).NotEmpty().WithMessage("Token Boş Olamaz").NotNull().WithMessage("Token Boş Olamaz");
        }
    }
}
