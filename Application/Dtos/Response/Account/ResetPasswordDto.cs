using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Response.Account
{
    public class ResetPasswordDto
    {
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfimPassword { get; set; }
        public string Token { get; set; }
    }
}
