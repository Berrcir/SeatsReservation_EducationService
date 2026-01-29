namespace SeatsReservationService.Domain.Events
{
    public class Event
    {
        public Guid Id { get; private set; }

        public Guid VenueId { get; private set; }

        public string Name { get; private set; }

        public DateTime Date { get; private set; }

        public EventDetails Details { get; private set; }

        public Event(Guid id, Guid venueId, string name, DateTime date, EventDetails details)
        {
            Id = id;
            VenueId = venueId;
            Name = name;
            Date = date;
            Details = details;
        }
    }
}
