using CSharpFunctionalExtensions;
using SeatsReservationService.Domain.Events;
using SeatsReservationService.Domain.Venues;
using System.Text.Json.Serialization;

namespace SeatsReservationService.Domain.Reservations
{
    public class Reservation
    {
        private List<ReservationSeat> _reservedSeats;

        public ReservationId Id { get; }

        public EventId EventId { get; private set; }

        public Guid UserId { get; private set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ReservationStatus Status { get; private set; }

        public DateTime CreatedAt { get; init; }

        public IReadOnlyList<ReservationSeat> ReservedSeats => _reservedSeats;

        // EF Core
        private Reservation()
        {
        }

        public Reservation(ReservationId id, Guid userId, EventId eventId, IEnumerable<SeatId> seatIds)
        {
            Id = id;
            UserId = userId;
            EventId = eventId;
            Status = ReservationStatus.Pending;
            CreatedAt = DateTime.UtcNow;

            List<ReservationSeat> reservedSeats = seatIds
                .Select(id => new ReservationSeat(new ReservationSeatId(Guid.NewGuid()), this, id))
                .ToList();

            _reservedSeats = reservedSeats;
        }
    }
}
