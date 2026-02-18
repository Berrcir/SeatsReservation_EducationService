using CSharpFunctionalExtensions;
using SeatsReservationService.Application.DataBase;
using SeatsReservationService.Contracts.Reservation;
using SeatsReservationService.Domain.Events;
using SeatsReservationService.Domain.Reservations;
using SeatsReservationService.Domain.Venues;
using Shared;

namespace SeatsReservationService.Application.Reservations
{
    /// <summary>
    /// Бронирование мест на мероприятие
    /// </summary>
    public class CreateReservationHandler
    {
        private readonly IReservationsRepository _reservationsRepository;
        private readonly IEventsRepository _eventsRepository;
        private readonly ISeatsRepository _seatsRepository;

        public CreateReservationHandler(
            IReservationsRepository reservationsRepository, 
            IEventsRepository eventsRepository,
            ISeatsRepository seatsRepository)
        {
            _reservationsRepository = reservationsRepository;
            _eventsRepository = eventsRepository;
            _seatsRepository = seatsRepository;
        }

        public async Task<Result<Guid, Error>> Handle(CreateReservationRequest request, CancellationToken cancellationToken)
        {
            var utcNow = DateTime.UtcNow;

            // Создать Reservation с ReservedSeats

            // Валидация входных параметров (request)

            var eventId = new EventId(request.EventId);
            var eventResult = await _eventsRepository.GetByIdAsync(eventId, cancellationToken);

            if (eventResult.IsFailure)
            {
                return eventResult.Error;
            }

            var @event = eventResult.Value;

            if (@event.IsAvailableForReservation(utcNow) == false)
            {
                return Error.Failure("reservation.unavailable", "Event is not available for reservation");
            }

            var seatIds = request.SeatIds.Select(id => new SeatId(id)).ToArray();
            var seats = await _seatsRepository.GetByIdsAsync(seatIds, cancellationToken);

            if (seats.Any(s => s.VenueId != @event.VenueId) || seats.Count == 0)
            {
                return Error.Conflict("reservation.seat.conflict", "Seat does not belong to venue");
            }

            bool hasUnavailableForReservationSeat = await _reservationsRepository.AnyAlreadyReservedSeat(@event.Id.Value, seatIds, cancellationToken);

            if (hasUnavailableForReservationSeat)
            {
                return Error.Failure("reservation.seat.unavailable", "Seat is not available for reservation");
            }

            var reservationResult = Reservation.Create(request.EventId, request.UserId, request.SeatIds);

            if (reservationResult.IsFailure)
            {
                return reservationResult.Error;
            }

            Reservation reservation = reservationResult.Value;

            await _reservationsRepository.Reservations.AddAsync(reservation, cancellationToken);

            return reservation.Id.Value;
        }
    }
}
