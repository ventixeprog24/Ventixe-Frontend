using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.SignUp
{
    public class AccountVerificationViewModel
    {
        [Required(ErrorMessage = "Please enter the verification code")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "The code must be 6 digits")]
        public string VerificationCode { get; set; } = null!;
        public string? Email { get; set; } 
        public string? VerificationToken { get; set; } 
    }
}
