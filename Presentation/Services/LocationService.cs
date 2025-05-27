using LocationServiceProvider;
using Presentation.Dtos;
using Presentation.Factories;
using Presentation.Models.Locations;
using System.Diagnostics;
using LocationServiceContractClient = LocationServiceProvider.LocationServiceContract.LocationServiceContractClient;

namespace Presentation.Services
{
    public class LocationService(LocationServiceContractClient locationService)
    {
        private readonly LocationServiceContractClient _locationService = locationService;

        public async Task<LocationServiceResult> CreateLocation(LocationViewModel viewModel)
        {
            try
            {
                var request = LocationFactory.ToCreateRequest(viewModel);

                var result = await _locationService.CreateLocationAsync(request);
                return result.Succeeded
                    ? new LocationServiceResult { Succeeded = true }
                    : new LocationServiceResult { Succeeded = false, ErrorMessage = result.ErrorMessage };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return new LocationServiceResult 
                { 
                    Succeeded = false, 
                    ErrorMessage = "An error occurred while creating the location. Please try again lateVer." 
                };
            }
        }

        public async Task<LocationServiceResult<List<LocationViewModel>>> GetAllLocations()
        {
            try
            {
                var result = await _locationService.GetAllLocationsAsync(new Empty());
                if (!result.Succeeded)
                    return new LocationServiceResult<List<LocationViewModel>> { Succeeded = false, ErrorMessage = result.ErrorMessage };

                if (result.Locations.Count == 0)
                    return new LocationServiceResult<List<LocationViewModel>> { Succeeded = true, Result = [] };

                var locationList = result.Locations.Select(LocationFactory.ToViewModel).ToList();
                return new LocationServiceResult<List<LocationViewModel>> { Succeeded = true, Result = locationList };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return new LocationServiceResult<List<LocationViewModel>> 
                { 
                    Succeeded = false,
                    ErrorMessage = "An error occurred while retrieving the locations. Please try again later."
                };
            }
        }

        public async Task<LocationServiceResult<LocationViewModel>> GetLocationById(string id)
        {
            try
            {
                var result = await _locationService.GetLocationByIdAsync(new LocationByIdRequest { Id = id });
                if (!result.Succeeded)
                    return new LocationServiceResult<LocationViewModel> { Succeeded = false, ErrorMessage = result.ErrorMessage };

                var location = LocationFactory.ToViewModel(result.Location);
                return new LocationServiceResult<LocationViewModel> { Succeeded = true, Result = location };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return new LocationServiceResult<LocationViewModel>
                {
                    Succeeded = false,
                    ErrorMessage = "An error occurred while retrieving the location. Please try again later."
                };
            }
        }

        public async Task<LocationServiceResult> UpdateLocation(LocationViewModel viewModel)
        {
            try
            {
                var request = LocationFactory.ToUpdateRequest(viewModel);

                var result = await _locationService.UpdateLocationAsync(request);
                return result.Succeeded
                    ? new LocationServiceResult { Succeeded = true }
                    : new LocationServiceResult { Succeeded = false, ErrorMessage = result.ErrorMessage };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return new LocationServiceResult 
                { 
                    Succeeded = false, 
                    ErrorMessage = "An error occurred while updating the location. Try again later." 
                };
            }
        }

        public async Task<LocationServiceResult> DeleteLocation(string id)
        {
            try
            {
                var result = await _locationService.DeleteLocationAsync(new LocationByIdRequest { Id = id });
                return result.Succeeded
                    ? new LocationServiceResult { Succeeded = true }
                    : new LocationServiceResult { Succeeded = false, ErrorMessage = result.ErrorMessage };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return new LocationServiceResult 
                { 
                    Succeeded = false, 
                    ErrorMessage = "An error occurred while deleting the location. Try again later." 
                };
            }
        }
    }
}
