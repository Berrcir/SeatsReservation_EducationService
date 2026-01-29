namespace SeatsReservationService.Domain.Reservations
{
    public class ReservationSeat
    {
        public Guid Id { get; }

        public Reservation Reservation { get; init; }

        public Guid SeatId { get; init; }

        public DateTime ReservedAt { get; init; }

        public ReservationSeat(Guid id, Reservation reservation, Guid seatId)
        {
            Id = id;
            Reservation = reservation;
            SeatId = seatId;
            ReservedAt = DateTime.UtcNow;
        }
    }
}
