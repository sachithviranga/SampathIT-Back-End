using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user.business;
using user.data;
using user.resources;
using user_backend.Extensions.Dependencies;

namespace user_backend.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            BusinessServiceCollection.RegisterServices(services, configuration);
            DataServiceCollection.RegisterServices(services, configuration);
            ResourceServiceCollection.RegisterServices(services);

            Authentication.SetAuthentication(services, configuration);
            Configuration.SetConfiguration(services, configuration);

            return services;
        }
    }
}
