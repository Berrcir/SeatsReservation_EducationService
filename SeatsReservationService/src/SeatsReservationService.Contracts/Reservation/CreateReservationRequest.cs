namespace SeatsReservationService.Contracts.Reservation
{
    public record class CreateReservationRequest(Guid EventId, Guid UserId, IEnumerable<Guid> SeatIds);
}
