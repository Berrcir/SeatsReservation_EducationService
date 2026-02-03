using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SeatsReservationService.Application.DataBase;
using SeatsReservationService.Domain.Venues;
using SeatsReservationService.Infrastructure.PostgreSql.Configurations;

namespace SeatsReservationService.Infrastructure.PostgreSql
{
    public class ReservationServiceDbContext : DbContext, IReservationServiceDbContext
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

            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.EnableSensitiveDataLogging();
            // optionsBuilder.LogTo(Console.WriteLine); // Прямой способ логирования
            optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VenueConfiguration).Assembly);
        }

        private ILoggerFactory CreateLoggerFactory() =>
            LoggerFactory.Create(builder => { builder.AddConsole(); });

        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        //{
        //    return await SaveChangesAsync(cancellationToken);
        //}
    }
}
