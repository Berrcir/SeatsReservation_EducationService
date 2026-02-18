using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SeatsReservationService.Domain.Events;
using SeatsReservationService.Domain.Reservations;
using SeatsReservationService.Domain.Venues;

namespace SeatsReservationService.Application.DataBase
{
    public interface IReservationServiceDbContext
    {
        public DbSet<Venue> Venues { get; }

        public DbSet<Reservation> Reservations { get; }

        public DbSet<Event> Events { get; }

        public DbSet<Seat> Seats { get; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        public ChangeTracker ChangeTracker { get; }
    }
}
