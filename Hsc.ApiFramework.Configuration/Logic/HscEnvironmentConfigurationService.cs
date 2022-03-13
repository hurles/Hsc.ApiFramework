using Hsc.ApiFramework.Enums;
using Hsc.ApiFramework.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsc.ApiFramework.Configuration.Logic
{
    public static class HscEnvironmentConfigurationService
    {
        public static string? GetSetting(HscSetting setting)
        {
            var settingValue = Environment.GetEnvironmentVariable(GetSettingText(setting));

            if (settingValue != null)
                return settingValue;
            else
                throw new HscConfigurationException($"Could not value for {setting}. Make sure the environment variable '{setting}' is set");
        }

        public static string GetSettingText(HscSetting setting)
        {
            switch (setting)
            {
                case HscSetting.HSC_AUTH_JWT_AUDIENCE:
                    return "HSC_AUTH_JWT_AUDIENCE";
                case HscSetting.HSC_AUTH_JWT_ISSUER:
                    return "HSC_AUTH_JWT_ISSUER";
                case HscSetting.HSC_AUTH_JWT_SECRET:
                    return "HSC_AUTH_JWT_SECRET";
                case HscSetting.HSC_DATABASE_CONNECTION:
                    return "HSC_DATABASE_CONNECTION";
                default:
                    return string.Empty;
            }
        }
    }

    public class HscConfigurationException : Exception
    {
        public HscConfigurationException(string? message) 
            : base(message)
        {

        }
    }
}
