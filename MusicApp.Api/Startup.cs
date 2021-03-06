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
using Microsoft.Extensions.FileProviders;
using System.Net;
using MusicApp.Api.Hubs;
using Microsoft.AspNetCore.Identity;
using MusicApp.Helper.Abstract;
using MusicApp.Helper.Concrate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MusicApp.Api.Localize;
using Microsoft.AspNetCore.Http.Features;

namespace MusicApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

 
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
                services.Configure<FormOptions>(o =>
                {
                    o.ValueLengthLimit = int.MaxValue;
                    o.MultipartBodyLengthLimit = int.MaxValue;
                    o.MemoryBufferThreshold = int.MaxValue;
                });

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ILogService, LogManager>();
            services.AddScoped<IMusicTypesRepository, MusicTypesRepository>();
            services.AddScoped<IMusicTypesService, MusicTypesManager>();
            services.AddScoped<IFilesRepository, FilesRepository>();
            services.AddScoped<IFilesService, FilesManager>();
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<IArtistService, ArtistManager>();
            services.AddScoped<IAlbumService, AlbumManager>();
            services.AddScoped<IAlbumRepository, AlbumRepository>();
            services.AddScoped<IMusicService, MusicManager>();
            services.AddScoped<IMusicRepository, MusicRepository>();
            services.AddScoped<IUserService, UserServiceManager>();
            services.AddScoped<IMailService, SendGridMailService>();
 

            services.AddScoped<IHelper, HelperManager>();

            services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

            services.AddAutoMapper(typeof(AutoMapping));


            services.AddCors(options =>
            {
                options.AddPolicy(name: "CorsPolicy",
                                  builder =>
                                  {
                                      builder
                                         .WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                                      builder
                                        .WithOrigins("http://localhost:3001").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                                      builder
                                      .WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                                  });
            });
            services.AddDbContext<MusicAppDbContext>(opt => opt.UseMySQL("server=localhost;port=3306;database=music_app;user=root;password="));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredUniqueChars = 1;

                options.SignIn.RequireConfirmedEmail = true;


                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                //options.Lockout.MaxFailedAccessAttempts = 5;
                //options.Lockout.AllowedForNewUsers = true;
            }).AddEntityFrameworkStores<MusicAppDbContext>().AddDefaultTokenProviders().AddErrorDescriber<MultilanguageIdentityErrorDescriber>();

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["SecretKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                };
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MusicApp Api",
                    Version = "1.0.0",
                    Description = "Custom MusicApp Api",
                    Contact = new OpenApiContact() { Email = "ysndlklc1234@gmail.com", Name = "Yasin Efem Dalk�l��", Url = new Uri("https://github.com/yasinfmd/"), }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });


            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "static";
            });
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MusicApp API V1");
                c.RoutePrefix = string.Empty;
            });
            //var b = Dns.GetHostEntry();

            app.UseAuthorization();
            // app.UseSignalR(x => x.MapHub<ChatHub>("/chat2"));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MusicTypesHub>("/mthub");
                endpoints.MapHub<ArtistHub>("/ahub");

            });

            app.UseStaticFiles(
                new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Uploads")),
                    RequestPath = "/Uploads"
                }
                );
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
