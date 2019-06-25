using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.DependencyInjection;

namespace Jobs.Security
{
    /// <summary>
    /// Called from Startup to load the custom binding when the Azure Functions host starts up.
    /// </summary>
    public static class AccessTokenExtensions
    {
        public static IFunctionsHostBuilder AddAccessTokenBinding(this IFunctionsHostBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Services.Add(ServiceDescriptor.Singleton<IExtensionConfigProvider, AccessTokenExtensionProvider>());

            return builder;
        }
    }
}