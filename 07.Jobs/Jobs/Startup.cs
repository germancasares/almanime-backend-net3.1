using Jobs;
using Infrastructure.Crosscutting;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.DependencyInjection;
using Domain.Configurations;
using Jobs.Security;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Jobs
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder = builder.AddAccessTokenBinding();

            var services = builder.Services;

            var connectionString = Environment.GetEnvironmentVariable("AlmanimeConnection");

            services
                .AddContext(connectionString)
                //.AddAuthentication(services.BuildServiceProvider().GetService<TokenConfiguration>())
                .AddServices()
                .AddRepositories()
                .AddMapper();
        }
    }
}