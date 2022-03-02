using Hsc.ApiFramework.Models.Identity.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Hsc.ApiFramework.Models.Identity.Implementation
{
    public class RegisterModel : IRegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}