using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.Locations
{
    public class LocationViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Location Name is required")]
        [DataType(DataType.Text)]
        [MinLength(2, ErrorMessage = "Must be at least 2 characters.")]
        [Display(Name = "Location Name", Prompt = "Enter Location Name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Address is required")]
        [DataType(DataType.Text)]
        [MinLength(2, ErrorMessage = "Must be at least 2 characters.")]
        [Display(Name = "Address", Prompt = "Enter Address")]
        public string StreetName { get; set; } = null!;

        [Required(ErrorMessage = "Postal Code is required")]
        [DataType(DataType.Text)]
        [MinLength(2, ErrorMessage = "Must be at least 2 characters.")]
        [Display(Name = "Postal Code", Prompt = "Enter Postal Code")]
        public string PostalCode { get; set; } = null!;

        [Required(ErrorMessage = "City is required")]
        [DataType(DataType.Text)]
        [MinLength(2, ErrorMessage = "Must be at least 2 characters.")]
        [Display(Name = "City", Prompt = "Enter City")]
        public string City { get; set; } = null!;

        [Range(0, int.MaxValue, ErrorMessage = "Cannot be negative.")]
        [Display(Name = "Number of Seats", Prompt = "Enter number of Seats")]
        public int SeatCount { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Cannot be negative.")]
        [Display(Name = "Number of Rows", Prompt = "Enter number of Rows")]
        public int RowCount { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Cannot be negative.")]
        [Display(Name = "Number of Gates", Prompt = "Enter number of Gates")]
        public int GateCount { get; set; }
    }
}
