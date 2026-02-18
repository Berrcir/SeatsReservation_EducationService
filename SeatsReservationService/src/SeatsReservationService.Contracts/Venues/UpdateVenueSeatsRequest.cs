namespace SeatsReservationService.Contracts.Venues
{
    public record class UpdateVenueSeatsRequest(Guid VenueId, IEnumerable<CreateSeatRequest> Seats);
}
