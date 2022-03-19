using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
            base.OnModelCreating(builder);
        }
    }
}