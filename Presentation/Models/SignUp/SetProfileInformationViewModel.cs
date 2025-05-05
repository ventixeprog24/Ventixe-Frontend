using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.SignUp
{
    public class SetProfileInformationViewModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "FirstName", Prompt = "Enter your first name")]
        [Required(ErrorMessage = "First name must be provided")]
        public string FirstName { get; set; } = null!;

        [DataType(DataType.Text)]
        [Display(Name = "LastName", Prompt = "Enter your last name")]
        [Required(ErrorMessage = "Last name must be provided")]
        public string LastName { get; set; } = null!;

        [DataType(DataType.Text)]
        [Display(Name = "PhoneNumber", Prompt = "Enter your phone number")]
        [Required(ErrorMessage = "Phone number must be provided")]
        public string PhoneNumber { get; set; } = null!;

        [DataType(DataType.Text)]
        [Display(Name = "Address", Prompt = "Enter your address")]
        public string? Address { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "PostalCode", Prompt = "Enter your postal code")]
        public string? PostalCode { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "City", Prompt = "Enter your City")]
        public string? City { get; set; }
    }
}
