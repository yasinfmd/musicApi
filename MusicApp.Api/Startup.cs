using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicApp.Logger.Abstract;
using MusicApp.Logger.Concrate;
using MusicApp.DataAccess;
using MusicApp.DataAccess.Abstract;
using MusicApp.DataAccess.Concrate;
using MusicApp.Business.Abstract;
using MusicApp.Business.Concrate;
using MusicApp.Api.Filter;
using MusicApp.Api.Extentions;
using MusicApp.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.IO;

namespace MusicApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
        


            services.AddControllers(options =>
            {
                options.Filters.Add<ValidationFilter>();
            }).AddMusicAppFluentValidation();

            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
            });

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ILogService, LogManager>();
            services.AddScoped<IMusicTypesRepository, MusicTypesRepository>();
            services.AddScoped<IMusicTypesService, MusicTypesManager>();
            services.AddAutoMapper(typeof(AutoMapping));


            services.AddCors(options=>
            {
                options.AddPolicy(name: "CorsPolicy",
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin()
                                      .AllowAnyHeader()
                                      .WithMethods("POST", "PUT", "DELETE", "GET");
                               
                                  });
            });
            services.AddDbContext<MusicAppDbContext>(opt => opt.UseMySQL("server=localhost;port=3306;database=music_app;user=root;password="));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MusicApp Api", Version = "1.0.0", Description = "Custom MusicApp Api",
                    Contact = new OpenApiContact() { Email = "ysndlklc1234@gmail.com",Name="Yasin Efem Dalk�l��", Url = new Uri("https://github.com/yasinfmd/"), } }) ;
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });
    

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "static"; 
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");


            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MusicApp API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                //endpoints.Map("/",  context =>
                //{
                   
                //    //send static file index.html
                //   // await context.Response.WriteAsync("Hello World");
                //});
            });
            app.Map("/musicApp", spaApp =>
            {
                spaApp.UseSpa(spa =>
                {
                    spaApp.UseStaticFiles("/static");
                });
            });
        }
    }
}
