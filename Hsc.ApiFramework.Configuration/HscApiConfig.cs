namespace Hsc.ApiFramework.Configuration
{
    public class HscApiConfig
    {
        public HscJwtSettings Authentication { get; set; } = new HscJwtSettings();
    }

    public class HscJwtSettings
    {
        public string Secret { get; set; } = Guid.NewGuid().ToString();
        public string Issuer { get; set; } = "https://localhost:5003/";
        public string Audience { get; set; } = "https://localhost:5003/";
    }
}