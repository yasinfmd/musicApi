using FluentValidation;
using MusicApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Validation
{
    public class FileValidator:AbstractValidator<Files>
    {
        public FileValidator()
        {
            RuleFor(file => file.ImageFile).NotNull().WithMessage("Dosya Boş Olamaz");
        }

    }
}
