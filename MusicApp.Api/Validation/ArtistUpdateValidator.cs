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
    public class ArtistUpdateValidator : AbstractValidator<ArtistUpdateModel>
    {
        private readonly IBaseRepository<Artist> _baseRepository;
        public ArtistUpdateValidator(IBaseRepository<Artist> baseRepository)
        {
            _baseRepository = baseRepository;
            CustomArtistValidation customArtistValidation = new CustomArtistValidation(_baseRepository);
            RuleFor(artist => artist.Name).NotNull().WithMessage("Artist Adı Boş Olamaz")
              .Length(3, 30).WithMessage("Artist  Adı 3 ile 30 Karakter Arasında Olmalıdır")
              .NotEmpty().WithMessage("Artist Adı Boş Olamaz");

            RuleFor(artist => artist.Gender).NotNull().WithMessage("Artist Açıklaması Boş Olamaz");

            RuleFor(artist => artist.Info).NotNull().WithMessage("Artist Açıklaması Boş Olamaz")
      .Length(100, 300).WithMessage("Aritst Açıklaması 100 ile 300 Karakter Arasında Olmalıdır")
      .NotEmpty().WithMessage("Artist Açıklaması Boş Olamaz");
        }
    }
}
