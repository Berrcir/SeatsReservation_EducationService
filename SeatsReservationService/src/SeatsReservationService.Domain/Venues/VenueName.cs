using CSharpFunctionalExtensions;
using SeatsReservationService.Domain.Constants;
using Shared;

namespace SeatsReservationService.Domain.Venues
{
    public record class VenueName
    {
        public required string Prefix { get; init; }

        public required string Name { get; init; }

        public UnitResult<Error> Create(string prefix, string name)
        {
            if (String.IsNullOrWhiteSpace(prefix))
            {
                return Error.Validation("venue.prefix", "Prefix cannot be empty or whitespace", nameof(VenueName.Prefix));
            }

            if (String.IsNullOrWhiteSpace(name))
            {
                return Error.Validation("venue.name", "Name cannot be empty or whitespace", nameof(VenueName.Name));
            }

            if (prefix.Length > LengthConstants.LENGTH_50)
            {
                return Error.Validation("venue.prefix", "Venue prefix is too long", nameof(VenueName.Prefix));
            }

            if (name.Length > LengthConstants.LENGTH_500)
            {
                return Error.Validation("venue.name", "Venue name is too long", nameof(VenueName.Name));
            }

            return UnitResult.Success<Error>();
        }

        public override string ToString()
            => $"{Prefix}-{Name}";
    }
}
