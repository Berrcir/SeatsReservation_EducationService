using SeatsReservationService.Domain.Venues;

namespace SeatsReservationService.Domain.Events
{
    public class Event
    {
        public EventId Id { get; }

        public VenueId VenueId { get; private set; }

        public string Name { get; private set; }

        public DateTime Date { get; private set; }

        public EventDetails Details { get; private set; }

        // EF Core
        private Event()
        {
        }

        public Event(EventId id, VenueId venueId, string name, DateTime date, EventDetails details)
        {
            Id = id;
            VenueId = venueId;
            Name = name;
            Date = date;
            Details = details;
        }
    }
}
