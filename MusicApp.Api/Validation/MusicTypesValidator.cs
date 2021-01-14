using FluentValidation;
using MusicApp.DataAccess.Abstract;
using MusicApp.Entity;

namespace MusicApp.Api.Validation
{
    public class MusicTypesValidator : AbstractValidator<MusicTypes>
    {
        private readonly IBaseRepository<MusicTypes> _baseRepository;
        public MusicTypesValidator(IBaseRepository<MusicTypes> baseRepository)
        {
            _baseRepository = baseRepository;
            CustomMusicTypeValidator customArtistValidation = new CustomMusicTypeValidator(_baseRepository);
            RuleFor(musicType => musicType.Name).NotNull().WithMessage("Müzik Tür Adı Boş Olamaz")
                .Length(3, 30).WithMessage("Müzik Tür Adı 3 ile 30 Karakter Arasında Olmalıdır")
                .NotEmpty().WithMessage("Müzik Tür Adı Boş Olamaz").Must(customArtistValidation.UniqueName).WithMessage("Müzik Türü Eklendi");
        }
    }
}
