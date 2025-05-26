using EventServiceProvider;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Events;
using Presentation.Services;

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
                TotalTickets = response.Event.TotalTickets,
                TicketsSold = response.Event.TicketsSold,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(Event eventDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _eventService.CreateEventAsync(eventDto);
            if (response.StatusCode != 200)
                return BadRequest(response.Message);
            return Ok(response);
        }

    }
}
