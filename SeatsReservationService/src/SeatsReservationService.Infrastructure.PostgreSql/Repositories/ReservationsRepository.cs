using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SeatsReservationService.Application.DataBase;
using SeatsReservationService.Domain.Reservations;
using SeatsReservationService.Domain.Venues;

namespace SeatsReservationService.Infrastructure.PostgreSql.Repositories
{
    public class ReservationsRepository : IReservationsRepository
    {
        private readonly IReservationServiceDbContext _dbContext;

        public DbSet<Reservation> Reservations => _dbContext.Reservations;

        public ReservationsRepository(IReservationServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AnyAlreadyReservedSeat(Guid eventId, IEnumerable<SeatId> seatIds, CancellationToken cancellationToken)
        {
            return await _dbContext.Reservations
                .Where(r => r.EventId.Value == eventId)
                .AnyAsync(r => r.ReservedSeats.Any(rs => seatIds.Contains(rs.SeatId)), cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
