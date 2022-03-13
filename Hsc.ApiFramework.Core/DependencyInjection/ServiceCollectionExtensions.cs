using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hsc.ApiFramework.Interfaces.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services)
        {         
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext<IdentityUser>>()
                .AddDefaultTokenProviders();
            return services;
        }

        public static IServiceCollection ConfigureIdentityServices<TIdentityDbContext>(this IServiceCollection services)
            where TIdentityDbContext : IdentityDbContext<IdentityUser>
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<TIdentityDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }

        public static IServiceCollection ConfigureIdentityServices<TIdentityDbContext, TUser>(this IServiceCollection services)
            where TIdentityDbContext : IdentityDbContext<TUser>
            where TUser : IdentityUser
        {
            services.AddIdentity<TUser, IdentityRole>()
                .AddEntityFrameworkStores<TIdentityDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }

        public static IServiceCollection ConfigureIdentityServices<TIdentityDbContext, TUser, TRole>(this IServiceCollection services)
            where TIdentityDbContext : IdentityDbContext<TUser>
            where TUser : IdentityUser
            where TRole : IdentityRole
        {
            services.AddIdentity<TUser, TRole>()
                .AddEntityFrameworkStores<TIdentityDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }
    }
}
