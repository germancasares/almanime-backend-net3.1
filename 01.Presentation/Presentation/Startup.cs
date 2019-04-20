using Domain.Configurations;
using FluentValidation.AspNetCore;
using Infrastructure.Crosscutting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

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
                .AddMvc()
                .AddFluentValidation();
                //.AddMvc(opt => {
                //    opt.Filters.Add(typeof(ValidatorActionFilter));
                //});

            services
                .AddConfiguration(Configuration)
                .AddContext()
                .AddIdentity()
                .AddAuthentication(services.BuildServiceProvider().GetService<TokenConfiguration>())
                .AddServices()
                .AddRepositories()
                .AddValidators()
                .AddMapper()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info
                    {
                        Title = "AlmBackend API",
                        Version = "v1",
                        Contact = new Contact
                        {
                            Email = "german.casares@outlook.com",
                            Name = "German Casares March",
                            Url = "https://www.linkedin.com/in/germancasares/"
                        },
                        Description = "Backend for the Almanime project."
                    });
                });
 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseCors("AllowAll");

            app.UseCors(builder => builder.WithOrigins(Configuration["FrontedUrl"]).AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AlmBackend API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
