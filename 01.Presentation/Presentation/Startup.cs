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
                .AddFluentValidation();

            services
                .AddConfiguration(Configuration)
                .AddContext()
                .AddIdentity()
                .AddAlmAuthentication()
                .AddServices()
                .AddRepositories()
                .AddValidators()
                .AddMapper()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "AlmBackend API",
                        Description = "Backend for the Almanime project.",
                        Version = "v1",
                        Contact = new OpenApiContact
                        {
                            Name = "German Casares March",
                            Url = new Uri("https://www.linkedin.com/in/germancasares/"),
                            Email = "german.casares@outlook.com",
                        },
                    });

                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.ApiKey,
                        Description = "Please Enter Authentication Token",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Flows = new OpenApiOAuthFlows
                        {
                            Implicit = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri("https://authorization.com"),
                                TokenUrl = new Uri("https://token.com"),
                                RefreshUrl = new Uri("https://refresh.com"),
                                Scopes = new Dictionary<string, string>(),
                            },
                        },
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,

                            },
                            new List<string>()
                        }
                    });

                    //c.DescribeAllEnumsAsStrings();
                });
 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AlmBackend API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseHttpsRedirection();

            app.UseCors(builder => builder.WithOrigins(Configuration["FrontendUrls"].Split(";")).AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
