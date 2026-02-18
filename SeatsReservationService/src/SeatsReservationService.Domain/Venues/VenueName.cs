using CSharpFunctionalExtensions;
using SeatsReservationService.Domain.Constants;
using Shared;
using System.Diagnostics.CodeAnalysis;

namespace SeatsReservationService.Domain.Venues
{
    public record class VenueName
    {
        public required string Prefix { get; init; }

        public required string Name { get; init; }

        [SetsRequiredMembers]
        private VenueName(string prefix, string name)
        {
            Prefix = prefix;
            Name = name;
        }

        public static Result<VenueName, Error> CreateWithoutPrefix(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                return Error.Validation("venue.name", "Name cannot be empty or whitespace", nameof(VenueName.Name));
            }

            if (name.Length > LengthConstants.LENGTH_500)
            {
                return Error.Validation("venue.name", "Venue name is too long", nameof(VenueName.Name));
            }

            return new VenueName(String.Empty, name);
        }

        public static Result<VenueName, Error> Create(string prefix, string name)
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

            return new VenueName(prefix, name);
        }

        public override string ToString()
            => $"{Prefix}-{Name}";
    }
}
