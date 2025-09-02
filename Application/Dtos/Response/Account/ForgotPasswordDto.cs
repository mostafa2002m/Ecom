using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Response.Account
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string WebLink { get; set; }
    }
}
