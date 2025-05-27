using EventServiceProvider;
using EventServiceContractClient = EventServiceProvider.EventContract.EventContractClient;
using CategoryServiceContractClient = EventServiceProvider.CategoryContract.CategoryContractClient;
using StatusServiceContractClient = EventServiceProvider.StatusContract.StatusContractClient;
using LocationServiceContractClient = LocationServiceProvider.LocationServiceContract.LocationServiceContractClient;
using LocationServiceProvider;

namespace Presentation.Services;

public interface IEventService
{
    Task<GetAllEventsReply> GetAllEventsAsync();
    Task<GetEventReply> GetEventByIdAsync(string eventId);
    Task<EventReply> CreateEventAsync(Event eventToAdd);
    Task<EventReply> DeleteEventAsync(string eventId);
    Task<EventReply> UpdateEventAsync(Event eventToUpdate);
    Task<GetAllCategoriesReply> GetAllCategoriesAsync();
    Task<LocationListReply> GetAllLocationsAsync();
    Task<GetAllStatusesReply> GetAllStatusesAsync();
}

public class EventService(EventServiceContractClient eventService, CategoryServiceContractClient categoryService, LocationServiceContractClient locationService, StatusServiceContractClient statusService) : IEventService
{
    private readonly EventServiceContractClient _eventService = eventService;
    private readonly CategoryServiceContractClient _categoryService = categoryService;
    private readonly LocationServiceContractClient _locationService = locationService;
    private readonly StatusServiceContractClient _statusService = statusService;

    public Task<GetAllEventsReply> GetAllEventsAsync()
    {
        var reply = _eventService.GetEvents(new Google.Protobuf.WellKnownTypes.Empty());
        return reply.Events.Count > 0
            ? Task.FromResult(reply)
            : Task.FromResult(new GetAllEventsReply
            {
                Exception = reply.Exception
            });
    }
    public Task<GetEventReply> GetEventByIdAsync(string eventId)
    {
        var reply = _eventService.GetEventById(new GetEventByIdRequest
        {
            EventId = eventId
        });
        return reply.Event is not null
            ? Task.FromResult(reply)
            : Task.FromResult(new GetEventReply
            {
                StatusCode = reply.StatusCode
            });
    }
    public Task<EventReply> CreateEventAsync(Event eventToAdd)
    {
        var reply = _eventService.AddEvent(eventToAdd);
        return reply.StatusCode == 200
            ? Task.FromResult(reply)
            : Task.FromResult(new EventReply
            {
                StatusCode = reply.StatusCode,
                Message = reply.Message
            });
    }

    public Task<EventReply> DeleteEventAsync(string eventId)
    {
        throw new NotImplementedException();
    }


    public Task<EventReply> UpdateEventAsync(Event eventToUpdate)
    {
        throw new NotImplementedException();
    }

    public Task<GetAllCategoriesReply> GetAllCategoriesAsync()
    {
        var reply = _categoryService.GetCategories(new Google.Protobuf.WellKnownTypes.Empty());
        return  Task.FromResult(reply);
  
    }
    public Task<LocationListReply> GetAllLocationsAsync()
    {
        var reply = _locationService.GetAllLocations(new LocationServiceProvider.Empty());
        return Task.FromResult(reply);
    }
    public Task<GetAllStatusesReply> GetAllStatusesAsync()
    {
        var reply = _statusService.GetStatuses(new Google.Protobuf.WellKnownTypes.Empty());
        return Task.FromResult(reply);
    }
}
