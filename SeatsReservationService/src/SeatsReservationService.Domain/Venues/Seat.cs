using CSharpFunctionalExtensions;
using Shared;

namespace SeatsReservationService.Domain.Venues
{
    public class Seat
    {
        public Guid Id { get; }

        public int RowNumber { get; private set; }

        public int SeatNumber { get; private set; }

        public Seat(Guid id, int rowNumber, int seatNumber)
        {
            Id = id;
            RowNumber = rowNumber;
            SeatNumber = seatNumber;
        }

        public static Result<Seat, Error> Create(int rowNumber, int seatNumber)
        {
            if (rowNumber <= 0)
            {
                return Error.Validation("seat.rowNumber", "Row number must be greater than zero", nameof(RowNumber));
            }

            if (seatNumber <= 0)
            {
                return Error.Validation("seat.seatNumber", "Seat number must be greater than zero", nameof(SeatNumber));
            }

            return new Seat(Guid.NewGuid(), rowNumber, seatNumber);
        }
    }
}
