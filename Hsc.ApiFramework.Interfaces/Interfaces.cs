using Hsc.ApiFramework.Enums;

namespace Hsc.ApiFramework.Interfaces
{
    public interface IHscConfigurationService
    {
        public string? GetSetting(HscSetting setting);
    }
}