using FluentValidation;
using MusicApp.DataAccess.Abstract;
using MusicApp.Entity;
using MusicApp.Entity.ParameterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Validation
{
    public class AlbumImageValidator : AbstractValidator<AlbumImagesModel>
    {
       // AlbumImagesModel
               private readonly IBaseRepository<Albums> _baseRepository;

        public AlbumImageValidator(IBaseRepository<Albums> baseRepository)
        {
            _baseRepository = baseRepository;
            CustomAlbumValidator customArtistValidation = new CustomAlbumValidator(_baseRepository);
            RuleFor(x => x.Name).NotNull().WithMessage("Albüm Adı Boş Olamaz")
               .Length(1, 40).WithMessage("Albüm  Adı 1 ile 30 Karakter Arasında Olmalıdır")
               .NotEmpty().WithMessage("Albüm Adı Boş Olamaz");
            RuleFor(x => x.ArtistId).NotNull().WithMessage("Artist  Boş Olamaz");
            RuleFor(x => x.Year).NotNull().WithMessage("Albüm Yılı  Boş Olamaz");


            RuleFor(x => x.Desc).NotNull().WithMessage("Albüm Açıklaması Boş Olamaz")
         .Length(100, 300).WithMessage("Albüm Açıklaması 100 ile 300 Karakter Arasında Olmalıdır")
         .NotEmpty().WithMessage("Albüm Açıklaması Boş Olamaz");

        }
    }
}
