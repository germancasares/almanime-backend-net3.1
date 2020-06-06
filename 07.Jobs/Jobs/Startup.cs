using Infrastructure.Crosscutting;
using Jobs;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Jobs
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var connectionString = Environment.GetEnvironmentVariable("AlmanimeConnection");

            builder.Services.AddJobsServices(connectionString);
        }
    }
}
