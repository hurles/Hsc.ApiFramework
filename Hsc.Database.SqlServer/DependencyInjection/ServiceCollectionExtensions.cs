using Hsc.ApiFramework.Configuration.Logic;
using Hsc.ApiFramework.Core.Database;
using Hsc.ApiFramework.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hsc.ApiFramework.Database.SqlServer.DependencyInjection
{
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
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static IServiceCollection AddHscDatabase<TDbContext>(this IServiceCollection services)
            where TDbContext : DbContext
        {
            var connectionString = Environment.GetEnvironmentVariable(HscEnvironmentConfigurationService.GetSettingText(HscSetting.HSC_DATABASE_CONNECTION));

            if (connectionString != null)
            {
                services.AddDbContext<TDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });
            }
            else
            {
                throw new Exception("HSC: Could not find connection string for SQL Server. Make sure the environment variable 'HSC_DATABASE_CONNECTION' is set");
            }
            return services;
        }

        /// <summary>
        /// <b>Adds default SQL database based off of the default HscDatabaseContext</b><br/><br/>
        /// <i>Uses the following environment variable as a connection string:</i>
        /// <br/>
        /// <br/>HSC_DATABASE_CONNECTION
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static IServiceCollection AddHscDatabase(this IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable(HscEnvironmentConfigurationService.GetSettingText(HscSetting.HSC_DATABASE_CONNECTION));

            if (connectionString != null)
            {
                services.AddDbContext<HscDatabaseContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });
            }
            else
            {
                throw new Exception("HSC: Could not find connection string for SQL Server. Make sure the environment variable 'HSC_DATABASE_CONNECTION' is set");
            }
            return services;
        }
    }
}