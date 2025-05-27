using EventServiceProvider;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Events;
using Presentation.Services;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Presentation.Controllers
{
    public class EventController(IEventService eventService) : Controller
    {
        private readonly IEventService _eventService = eventService;

        [Route("Home/events")]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Events";

            var response = await _eventService.GetAllEventsAsync();

            var eventList = response.Events?.Select(e => new EventViewModel
            {
                EventId = e.EventId,
                EventTitle = e.EventTitle,
                Description = e.Description,
                Date = e.Date?.ToDateTime(),
                Price = e.Price,
                BookingStatus = e.Status?.StatusName,
                Category = e.Category?.CategoryName,
                Location = e.Location?.Name,
                SeatCount = e.Location?.Seats?.Count ?? 0,
                TotalTickets = e.TotalTickets,
                TicketsSold = e.TicketsSold,
            }).ToList() ?? new List<EventViewModel>();

            return View(eventList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(string id)
        {
            var response = await _eventService.GetEventByIdAsync(id);

            if (response.Event == null)
            {
                return NotFound();
            }

            var model = new EventViewModel
            {
                EventId = response.Event.EventId,
                EventTitle = response.Event.EventTitle,
                Description = response.Event.Description,
                Date = response.Event.Date?.ToDateTime(),
                Price = response.Event.Price,
                BookingStatus = response.Event.Status?.StatusName,
                Category = response.Event.Category?.CategoryName,
                Location = response.Event.Location?.Name,
                LocationId = response.Event.Location?.Id,
                SeatCount = response.Event.Location?.Seats?.Count ?? 0,
                TotalTickets = response.Event.TotalTickets,
                TicketsSold = response.Event.TicketsSold,
            };

            return View(model);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _eventService.DeleteEventAsync(id);
            if (response.StatusCode != 200)
                return BadRequest(response.Message);
            return RedirectToAction("Index");
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            var categories = await _eventService.GetAllCategoriesAsync();
            var locations = await _eventService.GetAllLocationsAsync();
            var statuses = await _eventService.GetAllStatusesAsync();
            var viewModel = new CreateEventViewModel
            {
                Categories = categories.Categories.Select(c => new SelectListItem
                {
                    Value = c.CategoryId,
                    Text = c.CategoryName
                }).ToList() ?? new List<SelectListItem>(),

                Statuses = statuses.Statuses.Select(s => new SelectListItem
                {
                    Value = s.StatusId,
                    Text = s.StatusName
                }).ToList() ?? new List<SelectListItem>(),
                Locations = locations.Locations.Select(l => new SelectListItem
                {
                    Value = l.Id,
                    Text = l.Name
                }).ToList() ?? new List<SelectListItem>(),


            };

            return View(viewModel);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateEventViewModel model)
        {
            if (!ModelState.IsValid) 
                return View(model);

            var protoEvent = new Event
            {
                EventId = Guid.NewGuid().ToString(), // or your generated ID
                EventTitle = model.EventTitle,
                Description = model.Description,
                Date = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(model.Date?.ToUniversalTime() ?? DateTime.UtcNow),
                Price = model.Price,
                Location = new LocationServiceProvider.Location
                {
                    Id = model.SelectedLocationId,   
                },
                TotalTickets = model.TotalTickets,
                TicketsSold = model.TicketsSold,
                Status = new Status
                {
                    StatusId = model.SelectedStatusId,
                },
                Category = new Category
                {
                    CategoryId = model.SelectedCategoryId,
                }
            };

            var response = await _eventService.CreateEventAsync(protoEvent);
            if (response.StatusCode != 200)
                return BadRequest(response.Message);
            return RedirectToAction("Index");
        }

        [HttpGet("Update/{id}")]
        public async Task<IActionResult> Update(string id)
        {
            var response = await _eventService.GetEventByIdAsync(id);
            var categories = await _eventService.GetAllCategoriesAsync();
            var locations = await _eventService.GetAllLocationsAsync();
            var statuses = await _eventService.GetAllStatusesAsync();

            if (response.Event == null)
            {
                return NotFound();
            }

            var model = new CreateEventViewModel
            {
                EventTitle = response.Event.EventTitle,
                Description = response.Event.Description,
                Date = response.Event.Date?.ToDateTime(),
                Price = response.Event.Price,
                TotalTickets = response.Event.TotalTickets,
                SelectedCategoryId = response.Event.Category?.CategoryId,
                SelectedLocationId = response.Event.Location?.Id,
                SelectedStatusId = response.Event.Status?.StatusId,
                Categories = categories.Categories.Select(c => new SelectListItem
                {
                    Value = c.CategoryId,
                    Text = c.CategoryName
                }).ToList(),
                Locations = locations.Locations.Select(l => new SelectListItem
                {
                    Value = l.Id,
                    Text = l.Name
                }).ToList(),
                Statuses = statuses.Statuses.Select(s => new SelectListItem
                {
                    Value = s.StatusId,
                    Text = s.StatusName
                }).ToList()
            };

            return View(model); // It should render Update.cshtml
        }

        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update(string id, CreateEventViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var protoEvent = new Event
            {
                EventId = id,
                EventTitle = model.EventTitle,
                Description = model.Description,
                Date = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(model.Date?.ToUniversalTime() ?? DateTime.UtcNow),
                Price = model.Price,
                Location = new LocationServiceProvider.Location
                {
                    Id = model.SelectedLocationId,
                },
                TotalTickets = model.TotalTickets,
                TicketsSold = model.TicketsSold,
                Status = new Status
                {
                    StatusId = model.SelectedStatusId,
                },
                Category = new Category
                {
                    CategoryId = model.SelectedCategoryId,
                }
            };
            var response = await _eventService.UpdateEventAsync(protoEvent);
            if (response.StatusCode != 200)
                return BadRequest(response.Message);
            return RedirectToAction("Index");
        }


    }
}
