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
				x.ImplicitlyValidateChildProperties = true;
			});
			return mvcBuilder;
		}
	}
}
