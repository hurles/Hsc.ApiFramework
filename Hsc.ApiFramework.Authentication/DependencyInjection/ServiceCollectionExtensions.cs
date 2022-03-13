using Hsc.ApiFramework.Configuration;
using Hsc.ApiFramework.Configuration.DependencyInjection;
using Hsc.ApiFramework.Configuration.Logic;
using Hsc.ApiFramework.Core.Database;
using Hsc.ApiFramework.Enums;
using Hsc.ApiFramework.Interfaces;
using Hsc.ApiFramework.Interfaces.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsc.ApiFramework.Authentication.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// <b>Adds necessary services for HSC Framework Authentication</b>
        /// <br/><br/>
        /// <i>useAuthentication:</i> toggle whether default ASP.NET Core app.UseAuthentication() will be executed
        /// <br/>
        /// <i>useAuthorization:</i> toggle whether default ASP.NET Core app.UseAuthorization() will be executed
        /// </summary>
        /// <param name="app"></param>
        /// <param name="useAuthentication">if true, default ASP.NET Core app.UseAuthentication() will be executed, only set to false if this is already executed in another method</param>
        /// <param name="useAuthorization">if true, default ASP.NET Core app.UseAuthorization() will be executed, only set to false if this is already executed in another method</param>
        /// <returns></returns>     
        public static IApplicationBuilder UseHscAuthentication(this IApplicationBuilder app, bool useAuthentication = true, bool useAuthorization = true)
        {
            if (useAuthentication)
                app.UseAuthentication();

            if (useAuthorization)
                app.UseAuthorization();

            return app;
        }

        /// <summary>
        /// <b>Configure HSC Framework to use existing IdentityDbContext, IdentityUser and IdentityRole</b>
        /// <br/><br/>
        /// <i>Adds JWT token authentication using the following Environment variables:</i>
        /// <br/>
        /// <br/>HSC_JWT_AUDIENCE
        /// <br/>HSC_JWT_ISSUER
        /// <br/>HSC_JWT_SECRET
        /// </summary>
        /// <typeparam name="TIdentityDbContext">Pre-existing IdentityDbContext</typeparam>
        /// <typeparam name="TUser">Pre-existing IdentityUser</typeparam>
        /// <typeparam name="TRole">Pre-existing IdentityRole</typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddHscAuthentication<TIdentityDbContext, TUser, TRole>(this IServiceCollection services)
            where TIdentityDbContext : IdentityDbContext<TUser>
            where TUser : IdentityUser
            where TRole : IdentityRole
        {
            services.AddHscConfigurationServices();

            services.ConfigureIdentityServices<TIdentityDbContext, TUser, TRole>();

            services = ConfigureHscAuthenticationServices(services);
            return services;
        }

        /// <summary>
        /// <b>Configure HSC Framework to use existing IdentityDbContext and IdentityUser</b>
        /// <br/><br/>
        /// <i>Adds JWT token authentication using the following Environment variables:</i>
        /// <br/>
        /// <br/>HSC_JWT_AUDIENCE
        /// <br/>HSC_JWT_ISSUER
        /// <br/>HSC_JWT_SECRET
        /// </summary>
        /// <typeparam name="TIdentityDbContext">Pre-existing IdentityDbContext</typeparam>
        /// <typeparam name="TUser">Pre-existing IdentityUser</typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddHscAuthentication<TIdentityDbContext, TUser>(this IServiceCollection services)
            where TIdentityDbContext : IdentityDbContext<TUser>
            where TUser : IdentityUser
        {
            services.AddHscConfigurationServices();

            services.ConfigureIdentityServices<TIdentityDbContext, TUser>();

            services = ConfigureHscAuthenticationServices(services);
            return services;
        }

        /// <summary>
        /// <b>Configure HSC Framework to use existing IdentityDbContext</b>
        /// <br/><br/>
        /// <i>Adds JWT token authentication using the following Environment variables:</i>
        /// <br/>
        /// <br/>HSC_JWT_AUDIENCE
        /// <br/>HSC_JWT_ISSUER
        /// <br/>HSC_JWT_SECRET
        /// </summary>
        /// <typeparam name="TIdentityDbContext">Pre-existing IdentityDbContext</typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddHscAuthentication<TIdentityDbContext>(this IServiceCollection services) 
            where TIdentityDbContext : IdentityDbContext<IdentityUser>
        {
            services.AddHscConfigurationServices();

            services.ConfigureIdentityServices<TIdentityDbContext>();

            services = ConfigureHscAuthenticationServices(services);
            return services;
        }

        /// <summary>
        /// <b>Configure HSC Framework to use default AspNetCore IdentityDbContext, IdentityUser and IdentityRole</b>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddHscAuthentication(this IServiceCollection services)
        {
            services.AddHscConfigurationServices();

            services.ConfigureIdentityServices();

            services = ConfigureHscAuthenticationServices(services);
            return services;
        }

        private static IServiceCollection ConfigureHscAuthenticationServices(IServiceCollection services)
        {

            // Adding Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = HscEnvironmentConfigurationService.GetSetting(HscSetting.HSC_AUTH_JWT_AUDIENCE),
                    ValidIssuer = HscEnvironmentConfigurationService.GetSetting(HscSetting.HSC_AUTH_JWT_ISSUER),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(HscEnvironmentConfigurationService.GetSetting(HscSetting.HSC_AUTH_JWT_SECRET) ?? ""))
                };
            });
            return services;
        }
    }
}
