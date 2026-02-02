using Microsoft.EntityFrameworkCore;
using SeatsReservationService.Domain.Venues;
using SeatsReservationService.Infrastructure.PostgreSql.Configurations;

namespace SeatsReservationService.Infrastructure.PostgreSql
{
    public class ReservationServiceDbContext: DbContext
    {
        private readonly string _connectionString;

        public DbSet<Venue> Venues => Set<Venue>();

        public ReservationServiceDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VenueConfiguration).Assembly);
        }
    }
}
