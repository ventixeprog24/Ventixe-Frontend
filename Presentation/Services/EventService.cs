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

    public async Task<GetAllEventsReply> GetAllEventsAsync()
    {
        var reply = await _eventService.GetEventsAsync(new Google.Protobuf.WellKnownTypes.Empty());
        return reply.Events.Count > 0
            ? reply
            : new GetAllEventsReply
            {
                Exception = reply.Exception
            };
    }
    public async Task<GetEventReply> GetEventByIdAsync(string eventId)
    {
        var reply = await _eventService.GetEventByIdAsync(new GetEventByIdRequest
        {
            EventId = eventId
        });
        return reply.Event is not null
            ? reply
            : new GetEventReply
            {
                StatusCode = reply.StatusCode
            };
    }
    public async Task<EventReply> CreateEventAsync(Event eventToAdd)
    {
        var reply = await _eventService.AddEventAsync(eventToAdd);
        return reply.StatusCode == 200
            ? reply
            : new EventReply
            {
                StatusCode = reply.StatusCode,
                Message = reply.Message
            };
    }

    public async Task<EventReply> DeleteEventAsync(string eventId)
    {
        var reply = await _eventService.DeleteEventAsync(new DeleteEventRequest
        {
            EventId = eventId
        });
        return reply.StatusCode == 200
            ? reply
            : new EventReply
            {
                StatusCode = reply.StatusCode,
                Message = reply.Message
            };
    }


    public async Task<EventReply> UpdateEventAsync(Event eventToUpdate)
    {
        var reply = await _eventService.UpdateEventAsync(eventToUpdate);
        return reply.StatusCode == 200
            ? reply
            : new EventReply
            {
                StatusCode = reply.StatusCode,
                Message = reply.Message
            };
    }

    public async Task<GetAllCategoriesReply> GetAllCategoriesAsync()
    {
        var reply = await _categoryService.GetCategoriesAsync(new Google.Protobuf.WellKnownTypes.Empty());
        return reply;
  
    }
    public async Task<LocationListReply> GetAllLocationsAsync()
    {
        var reply = await _locationService.GetAllLocationsAsync(new LocationServiceProvider.Empty());
        return reply;
    }
    public async Task<GetAllStatusesReply> GetAllStatusesAsync()
    {
        var reply = await _statusService.GetStatusesAsync(new Google.Protobuf.WellKnownTypes.Empty());
        return reply;
    }
}
