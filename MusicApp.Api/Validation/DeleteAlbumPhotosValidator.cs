using FluentValidation;
using MusicApp.Entity.ParameterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Validation
{
    public class DeleteAlbumPhotosValidator: AbstractValidator<DeleteAlbumPhotosModel>
    {
        public DeleteAlbumPhotosValidator()
        {
            RuleFor(x => x.images).NotEmpty().WithMessage("Liste Boş Olamaz").NotNull().WithMessage("Liste Boş Olamaz");
        }
    }
}
