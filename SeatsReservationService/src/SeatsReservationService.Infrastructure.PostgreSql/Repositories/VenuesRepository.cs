using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SeatsReservationService.Application.DataBase;
using SeatsReservationService.Domain.Venues;
using Shared;

namespace SeatsReservationService.Infrastructure.PostgreSql.Repositories
{
    public class VenuesRepository : IVenuesRepository
    {
        private readonly IReservationServiceDbContext _dbContext;

        public DbSet<Venue> Venues => _dbContext.Venues;

        public VenuesRepository(IReservationServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Venue, Error>> GetByIdAsync(VenueId id, CancellationToken cancellationToken)
        {
            var venue = await _dbContext.Venues
                .Include(v => v.Seats)
                .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);

            if (venue is null)
            {
                return Error.NotFound("venue.not.found", $"Venue with id: {id.Value} not found");
            }

            var entries = _dbContext.ChangeTracker.Entries();

            return venue;
        }

        public async Task<IReadOnlyList<Venue>> GetByPrefixAsync(string prefix, CancellationToken cancellationToken)
        {
            var venues = _dbContext.Venues
                .Where(v => v.Name.Prefix.StartsWith(prefix))
                .ToArrayAsync(cancellationToken);

            return await venues;
        }

        public async Task<UnitResult<Error>> DeleteSeatsByVenueIdAsync(VenueId id, CancellationToken cancellationToken)
        {
            await _dbContext.Seats
                .Where(s => s.VenueId.Equals(id))
                .ExecuteDeleteAsync(cancellationToken);

            return UnitResult.Success<Error>();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
