using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Request.Account
{
    public class RegisterModelDto :LoginModelDto
    {
        [Required(ErrorMessage = "name is required")]
        public string Name { get; set; }

        [Required, Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
        //public string Role { get; set; } = "User";
    }

}
