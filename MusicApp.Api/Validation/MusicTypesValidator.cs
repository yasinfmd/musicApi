using FluentValidation;
using MusicApp.Entity;

namespace MusicApp.Api.Validation
{
    public class MusicTypesValidator : AbstractValidator<MusicTypes>
    {
        public MusicTypesValidator()
        {
            RuleFor(musicType => musicType.Name).NotNull().WithMessage("Müzik Tür Adı Boş Olamaz")
                .Length(3, 30).WithMessage("Müzik Tür Adı 3 ile 30 Karakter Arasında Olmalıdır")
                .NotEmpty().WithMessage("Müzik Tür Adı Boş Olamaz");
        }
    }
}
