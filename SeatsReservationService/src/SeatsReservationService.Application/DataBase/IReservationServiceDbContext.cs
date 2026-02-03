using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SeatsReservationService.Domain.Venues;

namespace SeatsReservationService.Application.DataBase
{
    public interface IReservationServiceDbContext
    {
        public DbSet<Venue> Venues { get; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        public ChangeTracker ChangeTracker { get; }
    }
}
