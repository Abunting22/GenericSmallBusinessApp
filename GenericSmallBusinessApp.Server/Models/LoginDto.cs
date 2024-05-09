using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GenericSmallBusinessApp.Server.Models
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; } = string.Empty;
    }
}
