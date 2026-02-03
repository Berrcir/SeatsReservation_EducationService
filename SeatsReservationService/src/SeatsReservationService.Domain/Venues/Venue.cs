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

        private Venue(VenueId id, VenueName name, int seatsLimit, IEnumerable<Seat> seats)
        {
            Id = id;
            Name = name;
            _seats = seats.ToList();
            SeatsLimit = seatsLimit;
        }

        public static Result<Venue, Error> Create(string prefix, string name, int seatsLimit, IEnumerable<Seat> seats)
        {
            if (seatsLimit <= 0)
            {
                return Error.Validation("venue.seatsLimit", "Seats limit must be greater than zero", nameof(SeatsLimit));
            }

            var venueNameResult = VenueName.Create(prefix, name);

            if (venueNameResult.IsFailure)
            {
                return venueNameResult.Error;
            }

            List<Seat> venueSeats = seats.ToList();

            if (!venueSeats.Any())
            {
                return Error.Validation("venue.seats", "Number of seats must be greater than zero", nameof(Seats));
            }

            if (venueSeats.Count > seatsLimit)
            {
                return Error.Validation("venue.seats", "Number of seats exceeds the venue`s seats limit", nameof(Seats));
            }

            return new Venue(new VenueId(Guid.NewGuid()), venueNameResult.Value, seatsLimit, venueSeats);
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
