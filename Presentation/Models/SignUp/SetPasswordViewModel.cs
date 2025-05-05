using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.SignUp
{
    public class SetPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "Password", Prompt = "Enter password")]
        [Required(ErrorMessage = "Password must be provided")]
        public string Password { get; set; } = null!;
    }
}
