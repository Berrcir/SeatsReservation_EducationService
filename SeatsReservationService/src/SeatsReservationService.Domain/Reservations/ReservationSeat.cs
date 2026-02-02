using SeatsReservationService.Domain.Venues;

namespace SeatsReservationService.Domain.Reservations
{
    public class ReservationSeat
    {
        public ReservationSeatId Id { get; }

        public Reservation Reservation { get; init; }

        public SeatId SeatId { get; init; }

        public DateTime ReservedAt { get; init; }

        // EF Core
        private ReservationSeat()
        {
        }

        public ReservationSeat(ReservationSeatId id, Reservation reservation, SeatId seatId)
        {
            Id = id;
            Reservation = reservation;
            SeatId = seatId;
            ReservedAt = DateTime.UtcNow;
        }
    }
}
