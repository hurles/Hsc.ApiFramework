using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hsc.Backend.Core.Database
{
    public class HscDatabaseContext : IdentityDbContext<IdentityUser>
    {
        public HscDatabaseContext(DbContextOptions options)
            : base(options) { }
    }
}