using Hsc.ApiFramework.Configuration.Logic;
using Hsc.ApiFramework.Enums;
using Hsc.ApiFramework.Enums.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hsc.ApiFramework.Core.Database
{
    /// <summary>
    /// Base class for Hsc.ApiFramework DbContext. Adds seed method for initial admin user.
    /// </summary>
    public class HscDatabaseContext : IdentityDbContext<IdentityUser>
    {
        /// <summary>
        /// Base class for Hsc.ApiFramework DbContext. Adds seed method for initial admin user.
        /// </summary>
        public HscDatabaseContext(DbContextOptions options)
            : base(options) { }

        /// <summary>
        /// Runs several methods to seed Hsc.ApiFramework initial database values (including initial admin password and username)
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new HscRoleConfiguration());

            var adminUserId = Guid.NewGuid();

            builder.ApplyConfiguration(new HscAdminConfiguration(adminUserId));
            builder.ApplyConfiguration(new HscUserRolesConfig(adminUserId));

            base.OnModelCreating(builder);
        }
    }

    /// <summary>
    /// Handles seeding of Identity Roles
    /// </summary>
    public class HscRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        /// <summary>
        /// Handles seeding of Identity Roles
        /// </summary>
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            var roles = new List<IdentityRole>();

            foreach (var role in Enum.GetValues<HscUserRoles>())
            {
                var roleName = role.GetRoleName();
                roles.Add(new IdentityRole
                {
                    Id = role.GetRoleKey(),
                    Name = roleName,
                    NormalizedName = roleName.ToUpperInvariant()
                });
            }


            builder.HasData(roles);
        }
    }

    /// <summary>
    /// Handles seeding of initial admin user
    /// </summary>
    public class HscAdminConfiguration : IEntityTypeConfiguration<IdentityUser>
    {
        private Guid adminId;

        /// <summary>
        /// Handles seeding of initial admin user
        /// </summary>
        /// <param name="adminId"></param>
        public HscAdminConfiguration(Guid adminId)
        {
            this.adminId = adminId;
        }

        /// <summary>
        /// Handles seeding of initial admin user
        /// </summary>
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            var userName = HscEnvironmentConfiguration.GetSetting(HscSetting.HSC_INITIAL_USERNAME);
            var password = HscEnvironmentConfiguration.GetSetting(HscSetting.HSC_INITIAL_PASSWORD);
            var email = HscEnvironmentConfiguration.GetSetting(HscSetting.HSC_INITIAL_USERNAME);
            var phone = HscEnvironmentConfiguration.GetSetting(HscSetting.HSC_INITIAL_PHONENUMBER);

            var admin = new IdentityUser
            {
                Id = adminId.ToString().ToUpperInvariant(),
                UserName = userName,
                NormalizedUserName = userName?.ToUpperInvariant(),
                Email = email,
                NormalizedEmail = email?.ToUpperInvariant(),
                PhoneNumber = phone ?? "XXXXXXXXXXXXX",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
            };

            admin.PasswordHash = GeneratePasswordHash(admin, password);

            builder.HasData(admin);
        }

        private string GeneratePasswordHash(IdentityUser user, string? password)
        {
            var passHash = new PasswordHasher<IdentityUser>();
            return passHash.HashPassword(user, password);
        }
    }

    /// <summary>
    /// Handles Role Assignment for initial user
    /// </summary>
    public class HscUserRolesConfig : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        private Guid adminId;

        /// <summary>
        /// Handles Role Assignment for initial user
        /// </summary>
        public HscUserRolesConfig(Guid adminId)
        {
            this.adminId = adminId;
        }

        /// <summary>
        /// Handles Role Assignment for initial user
        /// </summary>
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            IdentityUserRole<string> iur = new IdentityUserRole<string>
            {
                RoleId = HscUserRoles.SuperAdmin.GetRoleKey(),
                UserId = adminId.ToString().ToUpperInvariant()
            };

            builder.HasData(iur);
        }
    }
}