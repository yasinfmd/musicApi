using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using MusicApp.Api.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp.Api.Extentions
{
	public static class FluentValidationConfiguration
	{
		public static IMvcBuilder AddMusicAppFluentValidation(this IMvcBuilder mvcBuilder)
		{
			mvcBuilder.AddFluentValidation(x => {
				x.RegisterValidatorsFromAssemblyContaining<MusicTypesValidator>();
				x.RegisterValidatorsFromAssemblyContaining<FileValidator>();
				x.RegisterValidatorsFromAssemblyContaining<UpdatePhotoValidator>();
				x.RegisterValidatorsFromAssemblyContaining<ArtistImageValidator>();
				x.RegisterValidatorsFromAssemblyContaining<ArtistUpdateValidator>();
				x.RegisterValidatorsFromAssemblyContaining<AlbumImageValidator>();
				x.RegisterValidatorsFromAssemblyContaining<DeleteAlbumPhotosValidator>();
				x.RegisterValidatorsFromAssemblyContaining<AlbumValidator>();
				x.RegisterValidatorsFromAssemblyContaining<MusicValidator>();
				x.RegisterValidatorsFromAssemblyContaining<RegisterValidator>();
				x.RegisterValidatorsFromAssemblyContaining<LoginValidator>();
				x.RegisterValidatorsFromAssemblyContaining<ResetPasswordValidator>();
				x.RegisterValidatorsFromAssemblyContaining<NewPasswordValidator>();





				x.ImplicitlyValidateChildProperties = true;
			});
			return mvcBuilder;
		}
	}
}
