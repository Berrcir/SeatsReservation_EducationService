using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SeatsReservationService.Domain.Venues;
using Shared;

namespace SeatsReservationService.Application.DataBase
{
    public interface ISeatsRepository
    {
        DbSet<Seat> Seats { get; }

        Task<Result<Seat, Error>> GetByIdAsync(SeatId id, CancellationToken cancellationToken);

        Task<IReadOnlyList<Seat>> GetByIdsAsync(IEnumerable<SeatId> seatIds, CancellationToken cancellationToken);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}