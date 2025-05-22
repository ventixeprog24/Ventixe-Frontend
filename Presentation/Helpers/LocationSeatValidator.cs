using Presentation.Models.Locations;

namespace Presentation.Helpers
{
    public class LocationSeatValidator
    {
        public static string? ValidateSeatsWithRowsAndGates(LocationViewModel viewModel)
        {
            if (viewModel.SeatCount > 0 && (viewModel.RowCount <= 0 || viewModel.GateCount <= 0))
                return "Row and/or gates must be greater than 0 when seats are provided.";

            return null;
        }
    }
}
