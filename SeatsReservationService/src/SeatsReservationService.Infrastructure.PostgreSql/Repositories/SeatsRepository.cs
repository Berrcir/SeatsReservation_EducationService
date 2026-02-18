using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SeatsReservationService.Application.DataBase;
using SeatsReservationService.Domain.Venues;
using Shared;

namespace SeatsReservationService.Infrastructure.PostgreSql.Repositories
{
    public class SeatsRepository : ISeatsRepository
    {
        private readonly IReservationServiceDbContext _dbContext;

        public DbSet<Seat> Seats => _dbContext.Seats;

        public SeatsRepository(IReservationServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Seat, Error>> GetByIdAsync(SeatId id, CancellationToken cancellationToken)
        {
            var seat = await _dbContext.Seats
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

            if (seat is null)
            {
                return Error.NotFound("seat.not.found", $"Seat with id: {id.Value} not found");
            }

            var entries = _dbContext.ChangeTracker.Entries();

            return seat;
        }

        public async Task<IReadOnlyList<Seat>> GetByIdsAsync(IEnumerable<SeatId> seatIds, CancellationToken cancellationToken)
        {
            var seats = await _dbContext.Seats
                .Where(s => seatIds.Contains(s.Id))
                .ToListAsync(cancellationToken);

            return seats;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
