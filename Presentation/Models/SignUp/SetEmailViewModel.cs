using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace Presentation.Models.SignUp
{
    public class SetEmailViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", Prompt = "Enter email address")]
        [Required(ErrorMessage = "Email must be provided")]
        [RegularExpression("^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$", ErrorMessage = "Invalid e-mail format")]
        public string Email { get; set; } = null!;
    }
}
