using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using user.business.Managers;
using user.business.Mappers;
using user.business.Mappers.Common;
using user.business.Wrapper;
using user.contracts.Common;
using user.contracts.Managers;
using user.entities.Common;
using user.entities.Entities;

namespace user.business
{
    public static class BusinessServiceCollection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Managers
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<ITokenManager, TokenManager>();
            #endregion

            #region Mappers
            services.AddSingleton<IMapper<List<Message>, ServiceResponse>, ServiceResponseErrorMapper>();
            services.AddSingleton<IMapper<object, ServiceResponse>, ServiceResponseMapper>();
            services.AddSingleton<IMapper<UserCreateMapperWrapper, ApplicationUser>, UserCreateMapper>();
            services.AddSingleton<IMapper<AccessTokenMapperWrapper, TokenResponse>, AccessTokenMapper>();
            #endregion

            return services;
        }
    }
}
