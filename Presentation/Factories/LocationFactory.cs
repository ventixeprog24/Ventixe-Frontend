using LocationServiceProvider;
using Presentation.Models.Locations;

namespace Presentation.Factories
{
    public class LocationFactory
    {
        public static LocationViewModel ToViewModel(Location location)
        {
            return new LocationViewModel
            {
                Id = location.Id,
                Name = location.Name,
                StreetName = location.StreetName,
                City = location.City,
                PostalCode = location.PostalCode,
                SeatCount = location.Seats.Count,
                RowCount = location.Seats.Select(seat => seat.Row).Distinct().Count(),
                GateCount = location.Seats.Select(seat => seat.Gate).Distinct().Count(),
            };
        }

        public static LocationCreateRequest ToCreateRequest(LocationViewModel viewModel)
        {
            return new LocationCreateRequest
            {
                Name = viewModel.Name,
                StreetName = viewModel.StreetName,
                PostalCode = viewModel.PostalCode,
                City = viewModel.City,
                SeatCount = viewModel.SeatCount,
                RowCount = viewModel.RowCount,
                GateCount = viewModel.GateCount,
            };
        }

        public static LocationUpdateRequest ToUpdateRequest(LocationViewModel viewModel)
        {
            return new LocationUpdateRequest
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                StreetName = viewModel.StreetName,
                PostalCode = viewModel.PostalCode,
                City = viewModel.City,
                SeatCount = viewModel.SeatCount,
                RowCount = viewModel.RowCount,
                GateCount = viewModel.GateCount,
            };
        }
    }
}
