using Hsc.Backend.Models.Identity.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Hsc.Backend.Models.Identity.Implementation
{
    public class LoginModel : ILoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
