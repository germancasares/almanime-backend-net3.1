using System;
using Infrastructure.Crosscutting;
using Jobs;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Jobs
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var connectionString = Environment.GetEnvironmentVariable("AlmanimeConnection");

            //builder = builder.AddAccessTokenBinding();

            builder.Services
                .AddContext(connectionString)
                //.AddAuthentication(services.BuildServiceProvider().GetService<TokenConfiguration>())
                .AddServices()
                .AddRepositories()
                .AddMapper();
        }
    }
}