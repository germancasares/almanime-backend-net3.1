using FluentValidation.AspNetCore;
using Infrastructure.Crosscutting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presentation.ActionFilters;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using Domain.Options;
using System.Linq;
using Infrastructure.Helpers;

namespace AlmBackend
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
            services
                .AddControllers(opt =>
                {
                    opt.Filters.Add(typeof(ValidatorActionFilter));
                })
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .AddFluentValidation();

            services.AddBackendServices(Configuration);

            var swaggerOptions = Configuration
                .GetSection(SwaggerOptions.Accessor)
                .Get<SwaggerOptions>();

            services
                .AddSwaggerGen(c =>
                {
                    var doc = swaggerOptions.Doc;
                    c.SwaggerDoc(doc.Version, new OpenApiInfo
                    {
                        Title = doc.Title,
                        Description = doc.Description,
                        Version = doc.Version,
                        Contact = new OpenApiContact
                        {
                            Name = doc.Name,
                            Url = new Uri(doc.Url),
                            Email = doc.Email,
                        },
                    });

                    var req = swaggerOptions.SecurityRequirement;
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                In = EnumHelper.GetEnumFromString<ParameterLocation>(req.In).Value,
                                Name = req.Name,
                                Scheme = req.Scheme,
                                Reference = new OpenApiReference
                                {
                                    Type = EnumHelper.GetEnumFromString<ReferenceType>(req.Type).Value,
                                    Id = req.Id
                                },
                            },
                            new List<string>()
                        }
                    });

                    var def = swaggerOptions.SecurityDefinition;
                    c.AddSecurityDefinition(def.Scheme, new OpenApiSecurityScheme
                    {
                        Type = EnumHelper.GetEnumFromString<SecuritySchemeType>(def.Type).Value,
                        Description = def.Description,
                        Name = def.Name,
                        In = EnumHelper.GetEnumFromString<ParameterLocation>(def.In).Value,
                        Flows = new OpenApiOAuthFlows
                        {
                            Implicit = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri(def.AuthorizationUrl),
                                TokenUrl = new Uri(def.TokenUrl),
                                RefreshUrl = new Uri(def.RefreshUrl),
                                Scopes = new Dictionary<string, string>(),
                            },
                        },
                    });
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env, 
            IOptions<FrontendOptions> frontendOptions,
            IOptions<SwaggerOptions> swaggerOptions
        )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRouting();

            app.UseSwagger();

            var ui = swaggerOptions.Value.UI;
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(ui.EndpointUrl, ui.EndpointName);
                c.RoutePrefix = ui.RoutePrefix;
            });
            app.UseHttpsRedirection();

            app.UseCors(builder => builder.WithOrigins(frontendOptions.Value.Urls.ToArray()).AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
