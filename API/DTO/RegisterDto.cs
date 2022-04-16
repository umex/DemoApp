using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        [StringLength(10,MinimumLength = 2 , ErrorMessage = "Username must be between 2 and 10 characters long.")]
        public string Username { get; set; }

        [Required]
        [StringLength(20,MinimumLength = 4 , ErrorMessage = "Password must be between 4 and 20 characters long.")]
        public string Password { get; set; }

    }
}