using Hsc.ApiFramework.Enums;
using Hsc.ApiFramework.Enums.Extensions;
using Hsc.ApiFramework.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsc.ApiFramework.Configuration.Logic
{
    /// <summary>
    /// Helper class to get HSC environment variable settings
    /// </summary>
    public static class HscEnvironmentConfiguration
    {
        /// <summary>
        /// Gets setting value
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        /// <exception cref="HscConfigurationException"></exception>
        public static string? GetSetting(HscSetting setting)
        {
            var settingValue = Environment.GetEnvironmentVariable(setting.GetEnvironmentKey());

            if (settingValue != null || !setting.GetIsRequired())
                return settingValue;
            else
                throw new HscConfigurationException($"Could not find value for {setting}. Make sure the environment variable '{setting}' is set");
        }

        /// <summary>
        /// Gets setting and converts to double. Returns null when not able to convert
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static double? GetDoubleSetting(HscSetting setting)
        {
            var settingValue = Environment.GetEnvironmentVariable(setting.GetEnvironmentKey());

            if (settingValue != null && double.TryParse(settingValue, out var doubleValue))
                return doubleValue;

            return null;
        }        
    }

    /// <summary>
    /// Exception used for issues with Configuration
    /// </summary>
    public class HscConfigurationException : Exception
    {
        /// <summary>
        /// Create new HscConfigurationException
        /// </summary>
        /// <param name="message"></param>
        public HscConfigurationException(string? message) 
            : base(message)
        {

        }
    }
}
