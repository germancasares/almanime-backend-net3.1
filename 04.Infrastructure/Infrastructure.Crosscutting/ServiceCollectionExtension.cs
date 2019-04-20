using Application.Services;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.DTOs;
using Domain.Models;
using Domain.VMs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Data.Repositories;
using Persistence.Data.Repositories.Interfaces;

namespace Infrastructure.Crosscutting
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddContext(this IServiceCollection services) => services.AddDbContext<AlmanimeContext>(options => options.UseSqlServer("Name=AlmanimeConnection", b => b.MigrationsAssembly("Migrations.Data")));

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAnimeService, AnimeService>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAnimeRepository, AnimeRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.CreateMap<string, string>().ConvertUsing(s => string.IsNullOrWhiteSpace(s) ? null : s);

                // Anime
                config.CreateMap<AnimeDTO, Anime>();
                config.CreateMap<Anime, AnimeVM>()
                    .ForMember(a => a.CoverImage, opt => opt.MapFrom(src => src.CoverImageUrl))
                    .ForMember(a => a.PosterImage, opt => opt.MapFrom(src => src.PosterImageUrl));
            });

            return services;
        }

        //public static IServiceCollection AddIdentity(this IServiceCollection services)
        //{
        //    services
        //        .AddIdentity<IdentityUser, IdentityRole>()
        //        .AddEntityFrameworkStores<SecurityContext>()
        //        .AddDefaultTokenProviders();

        //    services.Configure<IdentityOptions>(options =>
        //    {
        //        options.User.RequireUniqueEmail = true;

        //        options.Password.RequireDigit = true;
        //        options.Password.RequiredLength = 6;

        //        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        //    });

        //    services.AddDbContext<SecurityContext>(options =>
        //        options.UseSqlServer("Name=SecurityConnection", b => b.MigrationsAssembly("Migrations.Security")));

        //    return services;
        //}

        //public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration config)
        //{
        //    services.AddAuthentication(options =>
        //    {
        //        options.DefaultAuthenticateScheme = "JwtBearer";
        //        options.DefaultChallengeScheme = "JwtBearer";
        //    }).AddJwtBearer("JwtBearer", jwtBearerOptions =>
        //    {
        //        jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey =
        //                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Security")["JwtKey"])),

        //            ValidateIssuer = true,
        //            ValidIssuer = config.GetSection("Security")["JwtIssuer"],

        //            ValidateAudience = true,
        //            ValidAudience = config.GetSection("Security")["JwtAudience"],

        //            ValidateLifetime = true, //validate the expiration and not before values in the token

        //            ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
        //        };
        //    });

        //    return services;
        //}
    }
}
