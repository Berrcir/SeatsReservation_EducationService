namespace SeatsReservationService.Contracts.Venues
{
    public record class CreateVenueRequest(string Name, string Prefix, int SeatsLimit, IEnumerable<CreateSeatRequest> Seats);
}
