using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SeatsReservationService.Domain.Events;
using Shared;

namespace SeatsReservationService.Application.DataBase
{
    public interface IEventsRepository
    {
        DbSet<Event> Events { get; }

        Task<Result<Event, Error>> GetByIdAsync(EventId id, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}