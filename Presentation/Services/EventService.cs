using EventServiceProvider;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using EventServiceContractClient = EventServiceProvider.EventContract.EventContractClient;

namespace Presentation.Services;

public interface IEventService
{
    Task<GetAllEventsReply> GetAllEventsAsync();
    Task<GetEventReply> GetEventByIdAsync(string eventId);
    Task<EventReply> CreateEventAsync(Event eventToAdd);
    Task<EventReply> DeleteEventAsync(string eventId);
    Task<EventReply> UpdateEventAsync(Event eventToUpdate);
}

public class EventService(EventServiceContractClient eventService) : IEventService
{
    private readonly EventServiceContractClient _eventService = eventService;


    public Task<GetAllEventsReply> GetAllEventsAsync()
    {
        var reply = _eventService.GetEvents(new Empty());
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
}
