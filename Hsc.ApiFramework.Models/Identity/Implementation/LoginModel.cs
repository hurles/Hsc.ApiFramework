using Hsc.ApiFramework.Models.Identity.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Hsc.ApiFramework.Models.Identity.Implementation
{
    public class LoginModel : ILoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
