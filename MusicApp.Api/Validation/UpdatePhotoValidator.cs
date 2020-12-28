using FluentValidation;
using MusicApp.Entity.ParameterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Validation
{
    public class UpdatePhotoValidator : AbstractValidator<UpdateProfilePhoto>
    {
        public UpdatePhotoValidator()
        {
            RuleFor(updateProfile => updateProfile.Id).NotNull().WithMessage("Artist Id  Boş Olamaz")
                .NotEmpty().WithMessage("Artist Id  Adı Boş Olamaz");

            RuleFor(updateProfile => updateProfile.File).NotNull().WithMessage("Artist Fotoğrafı Bulunamadı")
                .NotEmpty().WithMessage("Artist Fotoğrafı Bulunamadı");
        }
    }
}
