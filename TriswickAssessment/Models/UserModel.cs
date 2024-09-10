using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TriswickAssessment.Models
{
    public class UserModel
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string UserRole { get; set; }
    }

    
    public class Register
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class Login
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
