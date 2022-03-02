namespace Hsc.Backend.Models.Identity.Interfaces
{
    public interface ILoginModel
    {
        string? Password { get; set; }
        string? Username { get; set; }
    }
}