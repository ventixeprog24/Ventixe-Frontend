using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.SignUp
{
    public class SetPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "Password", Prompt = "Enter Password")]
        [Required(ErrorMessage = "Password must be provided")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password", Prompt = "Confirm Password")]
        [Compare(nameof(Password), ErrorMessage = "Passwords doesn't match!")]
        public string ConfirmPassword { get; set;} = null!;
    }
}
