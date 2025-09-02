using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Request.Account
{
    public class LoginModelDto
    {
        
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}
