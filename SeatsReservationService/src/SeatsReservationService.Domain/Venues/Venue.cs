using CSharpFunctionalExtensions;
using Shared;

namespace SeatsReservationService.Domain.Venues
{
    public class Venue
    {
        private List<Seat> _seats = [];

        private List<Guid> _eventIds = [];

        public VenueId Id { get; }

        public VenueName Name { get; private set; }

        public IReadOnlyList<Seat> Seats => _seats;

        public IReadOnlyList<Guid> EventIds => _eventIds;

        public int SeatsLimit { get; private set; }

        public int SeatsCount => _seats.Count;

        // EF Core
        private Venue()
        {
        }

        public Venue(VenueId id, VenueName name, IEnumerable<Seat> seats, int seatsLimit)
        {
            Id = id;
            Name = name;
            _seats = seats.ToList();
            SeatsLimit = seatsLimit;
        }

        public UnitResult<Error> AddSeat(Seat seat)
        {
            if (SeatsCount >= SeatsLimit)
            {
                return Error.Conflict("venue.seats.limit", "");
            }

            _seats.Add(seat);

            return UnitResult.Success<Error>();
        }

        public void ChangeSeatsLimit(int newSeatsLimit) => SeatsLimit = newSeatsLimit;
    }
}
