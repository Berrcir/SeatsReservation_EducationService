using CSharpFunctionalExtensions;
using SeatsReservationService.Application.DataBase;
using SeatsReservationService.Contracts.Venues;
using SeatsReservationService.Domain.Venues;
using Shared;

namespace SeatsReservationService.Application.Venues
{
    public class UpdateVenueSeatsHandler
    {
        private readonly IVenuesRepository _venuesRepository;
        private readonly ITransactionManager _transactionManager;

        public UpdateVenueSeatsHandler(IVenuesRepository venuesRepository, ITransactionManager transactionManager)
        {
            _venuesRepository = venuesRepository;
            _transactionManager = transactionManager;
        }

        /// <summary>
        /// Метод обновляет места на площадке
        /// </summary>
        /// <returns></returns>
        public async Task<UnitResult<Error>> Handle(UpdateVenueSeatsRequest request, CancellationToken cancellationToken)
        {
            var transactionScopeResult = await _transactionManager.BeginTransactionAsync(cancellationToken);

            if (transactionScopeResult.IsFailure)
            {
                return transactionScopeResult.Error;
            }

            using var transactionScope = transactionScopeResult.Value;

            // Валидация входных данных
            // Добавить FluentValidation

            // Бизнес валидация

            var venueId = new VenueId(request.VenueId);

            var venueResult = await _venuesRepository.GetByIdAsync(venueId, cancellationToken);

            if (venueResult.IsFailure)
            {
                transactionScope.Rollback();
                return venueResult.Error;
            }

            await _venuesRepository.DeleteSeatsByVenueIdAsync(venueId, cancellationToken);

            var venue = venueResult.Value;

            List<Seat> seats = new();

            foreach (CreateSeatRequest seatRequest in request.Seats)
            {
                var seatResult = Seat.Create(seatRequest.RowNumber, seatRequest.SeatNumber);

                if (seatResult.IsFailure)
                {
                    transactionScope.Rollback();
                    return seatResult.Error;
                }

                seats.Add(seatResult.Value);
            }

            venue.UpdateSeats(seats);

            var saveChangesResult = await _transactionManager.SaveChangesAsync(cancellationToken);

            if (saveChangesResult.IsFailure)
            {
                transactionScope.Rollback();
                return saveChangesResult.Error;
            }

            var commitResult = transactionScope.Commit();

            if (commitResult.IsFailure)
            {
                transactionScope.Rollback();
                return commitResult.Error;
            }

            return UnitResult.Success<Error>();
        }
    }
}
