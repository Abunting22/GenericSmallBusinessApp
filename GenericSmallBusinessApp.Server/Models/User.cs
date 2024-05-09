using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GenericSmallBusinessApp.Server.Models
{
    public class User
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RoleId { get; set; }
        public string JwtToken { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;
        [Required]
        [PasswordPropertyText]
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
