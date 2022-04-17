using Hsc.ApiFramework.Core.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hsc.ApiFramework.Interfaces.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add default Identity to HscDatabaseContext
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services)
        {         
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<HscDatabaseContext>()
                .AddDefaultTokenProviders();
            return services;
        }

        /// <summary>
        /// Add default Identity with custom IdentityDbContext
        /// </summary>
        /// <typeparam name="TIdentityDbContext"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureIdentityServices<TIdentityDbContext>(this IServiceCollection services)
            where TIdentityDbContext : IdentityDbContext<IdentityUser>
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<TIdentityDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }

        /// <summary>
        /// Add default Identity with custom IdentityUser
        /// </summary>
        /// <typeparam name="TIdentityDbContext"></typeparam>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureIdentityServices<TIdentityDbContext, TUser>(this IServiceCollection services)
            where TIdentityDbContext : IdentityDbContext<TUser>
            where TUser : IdentityUser
        {
            services.AddIdentity<TUser, IdentityRole>()
                .AddEntityFrameworkStores<TIdentityDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }

        /// <summary>
        /// Add default Identity with custom IdentityUser and IdentityDbContext
        /// </summary>
        /// <typeparam name="TIdentityDbContext"></typeparam>
        /// <typeparam name="TUser"></typeparam>
        /// <typeparam name="TRole"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
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
