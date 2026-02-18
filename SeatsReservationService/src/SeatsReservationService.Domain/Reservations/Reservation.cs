using CSharpFunctionalExtensions;
using SeatsReservationService.Domain.Events;
using SeatsReservationService.Domain.Venues;
using Shared;
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

        public Reservation(Guid userId, EventId eventId, IEnumerable<SeatId> seatIds)
        {
            Id = new ReservationId(Guid.NewGuid());
            UserId = userId;
            EventId = eventId;
            Status = ReservationStatus.Pending;
            CreatedAt = DateTime.UtcNow;

            List<ReservationSeat> reservedSeats = seatIds
                .Select(id => new ReservationSeat(new ReservationSeatId(Guid.NewGuid()), this, id))
                .ToList();

            _reservedSeats = reservedSeats;
        }

        public static Result<Reservation, Error> Create(
            Guid eventId,
            Guid userId,
            IEnumerable<Guid> seatIdsCollection)
        {
            if (eventId == Guid.Empty)
            {
                return Error.Validation("reservation.eventId", "Event Id can not be empty", nameof(eventId));
            }

            if (userId == Guid.Empty)
            {
                return Error.Validation("reservation.userId", "User Id can not be empty", nameof(userId));
            }

            List<SeatId> seatIds = seatIdsCollection?.Select(id => new SeatId(id)).ToList() ?? [];

            if (seatIds.Count == 0)
            {
                return Error.Validation("reservation.seats", "At least one seat must be selected", nameof(seatIdsCollection));
            }

            if (seatIds.Any(id => id.Value == Guid.Empty))
            {
                return Error.Validation("reservation.seats", "Seat Ids can not be empty", nameof(seatIdsCollection));
            }

            return new Reservation(userId, new EventId(eventId), seatIds);
        }
    }
}
