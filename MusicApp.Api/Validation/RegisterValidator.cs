﻿using FluentValidation;
using MusicApp.Entity.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Validation
{
    public class RegisterValidator: AbstractValidator<UserRegisterModel>
    {
        public RegisterValidator()
        {
            //custom olacak
            RuleFor(x => x.Email).NotNull().WithMessage("Email Adı Boş Olamaz")
                .EmailAddress().WithMessage("Lütfen Geçerli Bir Email Adresi Giriniz")
                .NotEmpty().WithMessage("Email  Adı Boş Olamaz");

            RuleFor(x => x.PhoneNumber).NotNull().WithMessage("Telefon Numarası Boş Olamaz").MinimumLength(11).WithMessage("Telefon Numarası 11 Karakterden Küçük Olamaz")
              .NotEmpty().WithMessage("Telefon Numarası Boş Olamaz");
            //custom olabilir
            RuleFor(x => x.Password).NotEmpty().WithMessage("Parola Boş Olamaz").MinimumLength(8).WithMessage("Minimumu 8 Karakter Olmalıdır");

        }

    }
}
