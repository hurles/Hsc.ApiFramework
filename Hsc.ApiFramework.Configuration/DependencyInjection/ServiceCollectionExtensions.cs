using Hsc.ApiFramework.Configuration.Logic;
using Hsc.ApiFramework.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsc.ApiFramework.Configuration.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHscConfigurationServices(this IServiceCollection services)
        {
            services.AddSingleton<IHscConfigurationService, HscEnvironmentConfigurationService>();

            return services;
        }
    }
}
