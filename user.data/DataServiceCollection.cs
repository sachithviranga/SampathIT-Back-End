using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using user.data.DatabaseContext;
using user.entities.Entities;
using Microsoft.EntityFrameworkCore;
using user.contracts.Common;
using user.data.Mappers;
using user.contracts.Repositories;
using user.data.Repositories;

namespace user.data
{
    public static class DataServiceCollection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Db Context
            services.AddDbContext<UserDbConext>(it =>
            {
                it.UseSqlServer(configuration["Database:ConnectionString"]);
            }, ServiceLifetime.Transient);

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var existingUserManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
            if (existingUserManager == null)
            {
                services.AddIdentity<ApplicationUser, IdentityRole>()
                        .AddEntityFrameworkStores<UserDbConext>()
                        .AddDefaultTokenProviders().AddTokenProvider("User", typeof(DataProtectorTokenProvider<ApplicationUser>));

            }
            #endregion

            #region Mappers
            services.AddScoped<IEntityMapper, EntityMapper>();
            #endregion

            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            return services;
        }
    }
}
