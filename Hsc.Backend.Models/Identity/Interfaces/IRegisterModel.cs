namespace Hsc.Backend.Models.Identity.Interfaces
{
    public interface IRegisterModel
    {
        string? Email { get; set; }
        string? Password { get; set; }
        string? Username { get; set; }
    }
}