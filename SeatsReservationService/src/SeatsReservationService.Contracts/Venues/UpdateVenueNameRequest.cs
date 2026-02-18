namespace SeatsReservationService.Contracts.Venues
{
    public record class UpdateVenueNameRequest(Guid Id, string Name);
}
