namespace SeatsReservationService.Domain.Events
{
    public class EventDetails
    {
        public EventId EventId { get; }

        public int Capacity { get; private set; }

        public string Description { get; private set; }

        // EF Core
        private EventDetails()
        {
        }

        public EventDetails(EventId id, int capacity, string description)
        {
            EventId = id;
            Capacity = capacity;
            Description = description;
        }
    }
}
