using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using user.contracts.Common;

namespace user.resources
{
    public static class ResourceServiceCollection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IErrorMessagesHandler, ErrorMessagesHandler>();

            return services;
        }
    }
}
