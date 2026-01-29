using CSharpFunctionalExtensions;

namespace SeatsReservationService.Domain.Reservations
{
    public class Reservation
    {
        private List<ReservationSeat> _reservedSeats;

        public Guid Id { get; }

        public Guid EventId { get; private set; }

        public Guid UserId { get; private set; }

        public ReservationStatus Status { get; private set; }

        public DateTime CreatedAt { get; init; }

        public IReadOnlyList<ReservationSeat> ReservedSeats => _reservedSeats;

        public Reservation(Guid id, Guid userId, Guid eventId, IEnumerable<Guid> seatIds)
        {
            Id = id;
            UserId = userId;
            EventId = eventId;
            Status = ReservationStatus.Pending;
            CreatedAt = DateTime.UtcNow;

            List<ReservationSeat> reservedSeats = seatIds
                .Select(id => new ReservationSeat(Guid.NewGuid(), this, id))
                .ToList();

            _reservedSeats = reservedSeats;
        }
    }
}
