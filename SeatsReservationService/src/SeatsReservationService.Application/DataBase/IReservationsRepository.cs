using Microsoft.EntityFrameworkCore;
using SeatsReservationService.Domain.Reservations;
using SeatsReservationService.Domain.Venues;

namespace SeatsReservationService.Application.DataBase
{
    public interface IReservationsRepository
    {
        DbSet<Reservation> Reservations { get; }

        Task<bool> AnyAlreadyReservedSeat(Guid eventId, IEnumerable<SeatId> seatIds, CancellationToken cancellationToken);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}