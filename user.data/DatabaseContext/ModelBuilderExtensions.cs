using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using user.common.Constant;

namespace user.data.DatabaseContext
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            #region Roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = Roles.User,
                    Name = "User",
                    NormalizedName = "User",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                }
            );

            #endregion
        }
    }
}
