using CSharpFunctionalExtensions;
using SeatsReservationService.Application.DataBase;
using SeatsReservationService.Contracts.Venues;
using SeatsReservationService.Domain.Venues;
using Shared;

namespace SeatsReservationService.Application.Venues
{
    public class CreateVenueHandler
    {
        private readonly IReservationServiceDbContext _reservationDbContext;

        public CreateVenueHandler(IReservationServiceDbContext reservationDbContext)
        {
            _reservationDbContext = reservationDbContext;
        }

        /// <summary>
        /// Метод создает площадку со всеми местами
        /// </summary>
        /// <returns></returns>
        public async Task<Result<Guid, Error>> Handle(CreateVenueRequest request, CancellationToken cancellationToken)
        {
            // Валидация входных данных
            // Добавить FluentValidation

            // Бизнес валидация

            // var seats = request.Seats.Select(s => Seat.Create(s.RowNumber, s.SeatNumber).Value); // Этот способ не подходит, потому что мы при создании должны проверить результат создания. Так делать мсожно если работаем через throw

            List<Seat> seats = [];

            foreach (CreateSeatRequest seatRequest in request.Seats)
            {
                var seatResult = Seat.Create(seatRequest.RowNumber, seatRequest.SeatNumber);

                if (seatResult.IsFailure)
                {
                    return seatResult.Error;
                }

                seats.Add(seatResult.Value);
            }

            var venueResult = Venue.Create(request.Prefix, request.Name, request.SeatsLimit, seats);

            if (venueResult.IsFailure)
            {
                return venueResult.Error;
            }

            await _reservationDbContext.Venues.AddAsync(venueResult.Value, cancellationToken);
            
            await _reservationDbContext.SaveChangesAsync(cancellationToken);

            return venueResult.Value.Id.Value;
        }        
    }
}
