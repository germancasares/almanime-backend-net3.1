using Application.Services;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.DTOs;
using Domain.Models;
using Domain.VMs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Persistence.Data;
using Persistence.Data.Repositories;
using Persistence.Data.Repositories.Interfaces;
using Persistence.Security.Core;
using System;
using System.Text;
using Application;
using Application.Interfaces;
using Domain.DTOs.Account;
using Presentation.Validators;
using FluentValidation;
using Domain.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Infrastructure.Crosscutting
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddContext(this IServiceCollection services) => services.AddDbContext<AlmanimeContext>(options => options.UseLazyLoadingProxies().UseSqlServer("Name=AlmanimeConnection", b => b.MigrationsAssembly("Migrations.Data")));
        public static IServiceCollection AddContext(this IServiceCollection services, string connectionString) => services.AddDbContext<AlmanimeContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Migrations.Data")));

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAnimeService, AnimeService>();
            services.AddScoped<IFansubService, FansubService>();
            services.AddScoped<ISubtitleService, SubtitleService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAnimeRepository, AnimeRepository>();
            services.AddScoped<IBaseRepository<Chapter>, BaseRepository<Chapter>>();
            services.AddScoped<IFansubRepository, FansubRepository>();
            services.AddScoped<IMembershipRepository, MembershipRepository>();
            services.AddScoped<IStorageRepository, StorageRepository>();
            services.AddScoped<IBaseRepository<Subtitle>, BaseRepository<Subtitle>>();
            services.AddScoped<ISubtitlePartialRepository, SubtitlePartialRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.CreateMap<string, string>().ConvertUsing(s => string.IsNullOrWhiteSpace(s) ? null : s);

                // Accounts
                config.CreateMap<RegisterDTO, IdentityUser>();

                // Anime
                config.CreateMap<AnimeDTO, Anime>();
                config.CreateMap<Anime, AnimeVM>()
                    .ForMember(a => a.CoverImage, opt => opt.MapFrom(src => src.CoverImageUrl))
                    .ForMember(a => a.PosterImage, opt => opt.MapFrom(src => src.PosterImageUrl));

                // Chapters
                config.CreateMap<Chapter, ChapterVM>();

                // Fansubs
                config.CreateMap<FansubDTO, Fansub>();
                config.CreateMap<Fansub, FansubVM>();

                // Users
                config.CreateMap<UserDTO, User>();

            }, AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<SecurityContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options => {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Password.RequiredUniqueChars = 0;

                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddDbContext<SecurityContext>(options => options.UseSqlServer("Name=SecurityConnection", b => b.MigrationsAssembly("Migrations.Security")));

            return services;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services, TokenConfiguration tokenConfiguration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.SaveToken = true;
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguration.Secret)),

                    ValidateIssuer = true,
                    ValidIssuer = tokenConfiguration.Issuer,

                    ValidateAudience = true,
                    ValidAudience = tokenConfiguration.Audience,

                    ValidateLifetime = true, //validate the expiration and not before values in the token

                    ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                };
            });

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<LoginDTO>, LoginDTOValidator>();
            services.AddTransient<IValidator<RegisterDTO>, RegisterDTOValidator>();
            services.AddTransient<IValidator<UserDTO>, UserDTOValidator>();

            return services;
        }

        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddOptions();

            var tokenConfiguration = new TokenConfiguration();
            config.Bind("Token", tokenConfiguration);
            services.AddSingleton(tokenConfiguration);

            return services;
        }
    }
}
