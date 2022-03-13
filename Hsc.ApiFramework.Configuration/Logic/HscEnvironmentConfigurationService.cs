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
    public class HscEnvironmentConfigurationService : IHscConfigurationService
    {
        public string? GetSetting(HscSetting setting)
        {
            return Environment.GetEnvironmentVariable(GetSettingText(setting));
        }

        private string GetSettingText(HscSetting setting)
        {
            switch (setting)
            {
                case HscSetting.HSC_AUTH_JWT_AUDIENCE:
                    return "HSC_AUTH_JWT_AUDIENCE";
                case HscSetting.HSC_AUTH_JWT_ISSUER:
                    return "HSC_AUTH_JWT_ISSUER";
                case HscSetting.HSC_AUTH_JWT_SECRET:
                    return "HSC_AUTH_JWT_SECRET";
                default:
                    return string.Empty;
            }
        }
    }
}
