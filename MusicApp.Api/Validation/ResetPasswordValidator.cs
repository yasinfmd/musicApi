using FluentValidation;
using MusicApp.Entity.ParameterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Validation
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordModel>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Boş Olamaz").EmailAddress().WithMessage("Geçerli Bir Email Adresi Giriniz").NotNull().WithMessage("Email Adresi Boş Olamaz");
        }

    }
}
