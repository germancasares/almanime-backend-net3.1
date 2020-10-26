using Application.Services;
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
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Linq;
using Presentation.Validators.FluentValidation;
using TokenOptions = Domain.Configurations.TokenOptions;
using Domain.Options;

namespace Infrastructure.Crosscutting
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBackendServices(this IServiceCollection services, IConfiguration config)
        {
            var tokenOptions = config
                .GetSection(TokenOptions.Accessor)
                .Get<TokenOptions>();

            services
                .AddConfiguration(config)
                .AddContext()
                .AddIdentity()
                .AddAlmAuthentication(tokenOptions)
                .AddRepositories()
                .AddServices()
                .AddValidators()
                .AddMapper();

            return services;
        }

        public static IServiceCollection AddJobsServices(this IServiceCollection services, string connectionString)
        {
            services
                .AddContext(connectionString)
                .AddRepositories()
                .AddServices()
                .AddMapper();

            return services;
        }

        private static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddOptions<FrontendOptions>()
                .Bind(config.GetSection(FrontendOptions.Accessor))
                .ValidateDataAnnotations();

            services.AddOptions<TokenOptions>()
                .Bind(config.GetSection(TokenOptions.Accessor))
                .ValidateDataAnnotations();

            services.AddOptions<SwaggerOptions>()
                .Bind(config.GetSection(SwaggerOptions.Accessor))
                .ValidateDataAnnotations();

            return services;
        }

        private static IServiceCollection AddContext(this IServiceCollection services, string connectionString = "Name=AlmanimeConnection") => services.AddDbContext<AlmanimeContext>(options => options.UseLazyLoadingProxies().UseSqlServer(connectionString, b => b.MigrationsAssembly("Migrations.Data")));

        private static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<SecurityContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
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

        private static IServiceCollection AddAlmAuthentication(this IServiceCollection services, TokenOptions tokenOptions)
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Secret)),

                    ValidateIssuer = true,
                    ValidIssuer = tokenOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = tokenOptions.Audience,

                    ValidateLifetime = true, //validate the expiration and not before values in the token

                    ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                };
            });

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAnimeService, AnimeService>();
            services.AddScoped<IBookmarkService, BookmarkService>();
            services.AddScoped<IEpisodeService, EpisodeService>();
            services.AddScoped<IFansubService, FansubService>();
            services.AddScoped<ISubtitleService, SubtitleService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAnimeRepository, AnimeRepository>();
            services.AddScoped<IBookmarkRepository, BookmarkRepository>();
            services.AddScoped<IEpisodeRepository, EpisodeRepository>();
            services.AddScoped<IFansubRepository, FansubRepository>();
            services.AddScoped<IMembershipRepository, MembershipRepository>();
            services.AddScoped<IStorageRepository, StorageRepository>();
            services.AddScoped<IBaseRepository<Subtitle>, BaseRepository<Subtitle>>();
            services.AddScoped<ISubtitlePartialRepository, SubtitlePartialRepository>();
            services.AddScoped<ISubtitleRepository, SubtitleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<FansubDTO>, FansubDTOValidator>();
            services.AddTransient<IValidator<LoginDTO>, LoginDTOValidator>();
            services.AddTransient<IValidator<RegisterDTO>, RegisterDTOValidator>();
            services.AddTransient<IValidator<UserDTO>, UserDTOValidator>();
            services.AddTransient<IValidator<SubtitleDTO>, SubtitleDTOValidator>();

            return services;
        }

        private static IServiceCollection AddMapper(this IServiceCollection services)
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
                config.CreateMap<Anime, AnimeWithEpisodesVM>()
                    .ForMember(a => a.CoverImage, opt => opt.MapFrom(src => src.CoverImageUrl))
                    .ForMember(a => a.PosterImage, opt => opt.MapFrom(src => src.PosterImageUrl));
                config.CreateMap<Anime, AnimeWithEpisodesAndSubtitleVM>()
                    .ForMember(a => a.EpisodesCount, opt => opt.MapFrom(src => src.Episodes.Count))
                    .ForMember(a => a.Episodes, opt => opt.MapFrom(src => src.Episodes.Where(s => s.Subtitles.Any(s => !string.IsNullOrEmpty(s.Url)))));
                config.CreateMap<Anime, FansubAnimeVM>()
                    .ForMember(a => a.CoverImage, opt => opt.MapFrom(src => src.CoverImageUrl));

                // Bookmarks
                config.CreateMap<BookmarkDTO, Bookmark>();
                config.CreateMap<Bookmark, BookmarkVM>()
                    .ForMember(b => b.AnimeSlug, opt => opt.MapFrom(src => src.Anime.Slug))
                    .ForMember(b => b.UserName, opt => opt.MapFrom(src => src.User.Name));

                // Episodes
                config.CreateMap<EpisodeDTO, Episode>();
                config.CreateMap<EpisodeDTO, EpisodeVM>();
                config.CreateMap<Episode, EpisodeVM>();
                config.CreateMap<Episode, EpisodeWithSubtitleVM>()
                    .ForMember(e => e.Subtitle, opt => opt.MapFrom(src => src.Subtitles.SingleOrDefault()));

                // Fansubs
                config.CreateMap<FansubDTO, Fansub>();
                config.CreateMap<Fansub, FansubVM>();

                // Subtitles
                config.CreateMap<Subtitle, SubtitleVM>()
                    .ForMember(s => s.ModificationDate, opt => opt.MapFrom(src => src.ModificationDate.HasValue ? src.ModificationDate : src.CreationDate));

                // Users
                config.CreateMap<UserDTO, User>();
                config.CreateMap<User, UserVM>()
                    .ForMember(u => u.Bookmarks, opt => opt.MapFrom(src => src.Bookmarks.Select(b => b.Anime.Slug)));

            }, AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
