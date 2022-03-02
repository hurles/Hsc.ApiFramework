namespace Hsc.Backend.Configuration
{
    public class ApiConfig
    {
        public JwtSettings Authentication { get; set; } = new JwtSettings();
    }

    public class JwtSettings
    {
        public string Secret { get; set; } = "HI7NtbXg0bpCa3AqmLIInmIJrYPVSy1PR3Apy5fP";
        public string Issuer { get; set; } = "https://localhost:5003/";
        public string Audience { get; set; } = "https://localhost:5003/";
    }
}