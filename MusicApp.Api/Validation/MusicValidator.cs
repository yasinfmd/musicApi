using FluentValidation;
using MusicApp.DataAccess.Abstract;
using MusicApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Validation
{
    public class MusicValidator : AbstractValidator<Music>
    {
        private readonly IBaseRepository<Music> _baseRepository;
        public MusicValidator(IBaseRepository<Music> baseRepository)
        {
            _baseRepository = baseRepository;
            CustomMusicValidator customMusicValidator = new CustomMusicValidator(_baseRepository);
            RuleFor(music => music.AlbumId).NotNull().WithMessage("Albüm Boş Olamaz")
              .NotEmpty().WithMessage("Albüm Boş Olamaz");
            RuleFor(music => music.MusicTypesId).NotNull().WithMessage("Müzik Türü Boş Olamaz")
              .NotEmpty().WithMessage("Müzik Türü Boş Olamaz");
            RuleFor(music => music.Minute).NotNull().WithMessage("Dakika Boş Olamaz")
                .Length(1,2).WithMessage("Max 2 karakter olabilir")
                .NotEmpty().WithMessage("Dakika Boş Olamaz");
            RuleFor(music => music.Second).NotNull().WithMessage("Saniye Boş Olamaz")
                .Length(1,2).WithMessage("Max 2 karakter olabilir")
                .NotEmpty().WithMessage("Saniye Boş Olamaz");
            RuleFor(music => music.Name).NotNull().WithMessage("Şarkı Adı Boş Olamaz")
               .Length(2,20).WithMessage("Şarkı Adı 2 ile 50 Karakter Arasında Olabilir")
               .NotEmpty().WithMessage("Şarkı Adı Boş Olamaz");

            RuleFor(music => music).Must(customMusicValidator.UniqueMusicNameOfAlbum).WithMessage("Bu Albüme Ait Böyle Bir Şarkı Var");
          
        }
    }
}
