using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SeatsReservationService.Application.DataBase;
using SeatsReservationService.Domain.Events;
using SeatsReservationService.Domain.Venues;
using Shared;

namespace SeatsReservationService.Infrastructure.PostgreSql.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private readonly IReservationServiceDbContext _dbContext;

        public DbSet<Event> Events => _dbContext.Events;

        public EventsRepository(IReservationServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Event, Error>> GetByIdAsync(EventId id, CancellationToken cancellationToken)
        {
            var @event = await _dbContext.Events
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

            if (@event is null)
            {
                return Error.NotFound("event.not.found", $"Event with id: {id.Value} not found");
            }

            var entries = _dbContext.ChangeTracker.Entries();

            return @event;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
