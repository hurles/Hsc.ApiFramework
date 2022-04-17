using Hsc.ApiFramework.Configuration.Logic;
using Hsc.ApiFramework.Core.Database;
using Hsc.ApiFramework.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Hsc.ApiFramework.Database.SqlServer.DependencyInjection
{
    /// <summary>
    /// Extension methods for Database
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// <b>Adds SQL database to entity framework for a given DbContext type</b><br/><br/>
        /// 
        /// <i>Uses the following environment variable as a connection string:</i>
        /// <br/>
        /// <br/>HSC_DATABASE_CONNECTION
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="migrationsAssembly">migration assembly name</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static IServiceCollection AddHscDatabase<TDbContext>(this IServiceCollection services, string? migrationsAssembly = null)
            where TDbContext : DbContext
        {
            var connectionString = HscEnvironmentConfiguration.GetSetting(HscSetting.HSC_DATABASE_CONNECTION) ?? "";
            var migrationsAssemblyEnv = HscEnvironmentConfiguration.GetSetting(HscSetting.HSC_MIGRATIONS_ASSEMBLY);


            services.AddDbContext<TDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlServerOptions =>
                {
                    if (!string.IsNullOrWhiteSpace(migrationsAssemblyEnv) || !string.IsNullOrWhiteSpace(migrationsAssembly))
                        sqlServerOptions.MigrationsAssembly(migrationsAssemblyEnv ?? migrationsAssembly);

                    sqlServerOptions.EnableRetryOnFailure(
                                maxRetryCount: 10,
                                maxRetryDelay: TimeSpan.FromSeconds(30),
                                errorNumbersToAdd: null);
                    }
                );
            });

            return services;
        }

        /// <summary>
        /// <b>Adds default SQL database based off of the default HscDatabaseContext</b><br/><br/>
        /// <i>Uses the following environment variable as a connection string:</i>
        /// <br/>
        /// <br/>HSC_DATABASE_CONNECTION
        /// </summary>
        /// <param name="services"></param>
        /// <param name="migrationsAssembly">name of migrations assembly</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static IServiceCollection AddHscDatabase(this IServiceCollection services, string? migrationsAssembly = null)
        {
            var connectionString = HscEnvironmentConfiguration.GetSetting(HscSetting.HSC_DATABASE_CONNECTION);
            var migrationsAssemblyEnv = HscEnvironmentConfiguration.GetSetting(HscSetting.HSC_MIGRATIONS_ASSEMBLY);

            services.AddDbContext<HscDatabaseContext>(options =>
            {
                options.UseSqlServer(connectionString!, sqlServerOptions =>
                {
                    if (!string.IsNullOrWhiteSpace(migrationsAssemblyEnv) || !string.IsNullOrWhiteSpace(migrationsAssembly))
                        sqlServerOptions.MigrationsAssembly(migrationsAssemblyEnv ?? migrationsAssembly);
                    sqlServerOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });
                     
            });

            return services;
        }
    }
}