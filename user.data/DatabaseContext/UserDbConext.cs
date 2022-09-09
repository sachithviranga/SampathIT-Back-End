using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using user.entities.Entities;

namespace user.data.DatabaseContext
{
    public class UserDbConext : IdentityDbContext<ApplicationUser>
    {
        public UserDbConext(DbContextOptions<UserDbConext> options)
         : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Seed();
            base.OnModelCreating(builder);

            Assembly assemblyWithConfigurations = GetType().Assembly;
            builder.ApplyConfigurationsFromAssembly(assemblyWithConfigurations);
            var authSchema = "Auth";

            #region MemberShip Entities
            builder.Entity<ApplicationUser>(entity => { entity.ToTable(name: $"{authSchema}_T_Users", schema: authSchema.ToLower()); });
            builder.Entity<IdentityRole>(entity => { entity.ToTable(name: $"{authSchema}_T_Roles", schema: authSchema.ToLower()); });
            builder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable(name: $"{authSchema}_T_UserRoles", schema: authSchema.ToLower()); });
            builder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable(name: $"{authSchema}_T_UserClaims", schema: authSchema.ToLower()); });
            builder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable(name: $"{authSchema}_T_UserLogins", schema: authSchema.ToLower()); });
            builder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable(name: $"{authSchema}_T_UserTokens", schema: authSchema.ToLower()); });
            builder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable(name: $"{authSchema}_T_RoleClaims", schema: authSchema.ToLower()); });
            builder.Entity<ApplicationUserImage>(entity => { entity.ToTable(name: $"{authSchema}_T_UserImages", schema: authSchema.ToLower()); });
            #endregion



            #region Entities
            #endregion

        }
    }
}
