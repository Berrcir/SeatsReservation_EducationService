using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SeatsReservationService.Domain.Venues;
using Shared;

namespace SeatsReservationService.Application.DataBase
{
    public interface IVenuesRepository
    {
        DbSet<Venue> Venues { get; }

        Task<UnitResult<Error>> DeleteSeatsByVenueIdAsync(VenueId id, CancellationToken cancellationToken);
        Task<Result<Venue, Error>> GetByIdAsync(VenueId id, CancellationToken cancellationToken);
        Task<IReadOnlyList<Venue>> GetByPrefixAsync(string prefix, CancellationToken cancellationToken);
        public Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}