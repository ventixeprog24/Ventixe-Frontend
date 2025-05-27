namespace Presentation.Models.Events;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

public class CreateEventViewModel
{
    public string? Image { get; set; }
    [Display(Name = "EventTitle", Prompt = "Enter title")]
    [Required(ErrorMessage = "Title must be provided")]
    public string EventTitle { get; set; } = null!;
    [Display(Name = "Description", Prompt = "Enter description")]
    [Required(ErrorMessage = "Description must be provided")]
    public string Description { get; set; } = null!;
    public DateTime? Date { get; set; }
    [Display(Name = "Price", Prompt = "Enter price")]
    [Required(ErrorMessage = "Price must be provided")]
    public int Price { get; set; }
    [Display(Name = "Number of Tickets", Prompt = "Enter total tickets")]
    public int TotalTickets { get; set; } = 0;
    public int TicketsSold { get; set; } = 0;
    [Display(Name = "Number of Tickets", Prompt = "Enter total tickets")]
    public string? SelectedStatusId { get; set; }
    public List<SelectListItem> Statuses { get; set; } = new();
    [Display(Name = "Number of Tickets", Prompt = "Enter total tickets")]

    public string? SelectedCategoryId { get; set; }
    public List<SelectListItem> Categories { get; set; } = new ();
    [Display(Name = "Number of Tickets", Prompt = "Enter total tickets")]

    public string? SelectedLocationId { get; set; }
    public List<SelectListItem> Locations { get; set; } = new();
}
