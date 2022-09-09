using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user.entities.Common.Configuration;

namespace user_backend.Extensions.Dependencies
{
    public class Configuration
    {
        public static void SetConfiguration(IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<AuthenticationConfiguration>(configuration.GetSection("Authentication"));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
