using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Response.Account
{
    public class ChangePasswordDto
    {
        public string UserName { get; set; }
        [Required(ErrorMessage = "Current Password Required")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "New Password Required")]
        public string NewPassword { get; set; }
        [Compare("NewPassword")]
        public string ComfirmPassword { get; set; }
    }
}
