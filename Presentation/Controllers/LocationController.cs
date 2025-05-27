using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Locations;
using Presentation.Services;

namespace Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LocationController(LocationService locationService) : Controller
    {
        private readonly LocationService _locationService = locationService;

        [Route("home/locations")]
        public async Task<IActionResult> Index()
        {
            var locations = await _locationService.GetAllLocations();
            if (!locations.Succeeded)
            {
                ViewBag.ErrorMessage = locations.ErrorMessage;
                return View(new List<LocationViewModel>());
            }

            return View(locations.Result);
        }

        [HttpGet("locations/create")]
        public IActionResult Create()
        {
            return View("Create", new LocationViewModel());
        }

        [HttpPost("locations/create")]
        public async Task<IActionResult> Create(LocationViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("Create", viewModel);

            var created = await _locationService.CreateLocation(viewModel);
            if (!created.Succeeded)
            {
                ViewBag.ErrorMessage = created.ErrorMessage;
                return View("Create", viewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("locations/edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            var location = await _locationService.GetLocationById(id);
            if (!location.Succeeded)
                RedirectToAction(nameof(Index));

            return View(location.Result);
        }

        [HttpPost("locations/edit/{id}")]
        public async Task<IActionResult> Edit(string id, LocationViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                ViewBag.ErrorMessage = "The location ID is invalid. Please refresh the page and try again.";
                return View("Edit", viewModel);
            }

            if (!ModelState.IsValid)
                return View("Edit", viewModel);

            var updated = await _locationService.UpdateLocation(viewModel);
            if (!updated.Succeeded)
            {
                ViewBag.ErrorMessage = updated.ErrorMessage;
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost("locations/delete")]
        public async Task<IActionResult> Delete([FromForm] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                TempData["ErrorMessage"] = "The location ID is invalid. Please refresh the page and try again.";
                return RedirectToAction(nameof(Index));
            }

            var deleted = await _locationService.DeleteLocation(id);
            if (!deleted.Succeeded)
            {
                TempData["ErrorMessage"] = deleted.ErrorMessage;
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
