using Infrastructure.Crosscutting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Functions
{
    public class DependencyInjection
    {
        private static ServiceProvider services;
        public static ServiceProvider Services
        {
            get
            {
                if (services == null)
                    services = CreateServiceProvider();

                return services;
            }
        }

        private static ServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();

            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .Build();

            services
                .AddSingleton<IConfiguration>(configuration)
                .AddContext()
                .AddServices()
                .AddRepositories()
                .AddMapper();

            return services.BuildServiceProvider();
        }
    }
}
