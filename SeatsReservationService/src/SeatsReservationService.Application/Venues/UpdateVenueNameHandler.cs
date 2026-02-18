using CSharpFunctionalExtensions;
using SeatsReservationService.Application.DataBase;
using SeatsReservationService.Contracts.Venues;
using SeatsReservationService.Domain.Venues;
using Shared;

namespace SeatsReservationService.Application.Venues
{
    public class UpdateVenueNameHandler
    {
        private readonly IVenuesRepository _venuesRepository;

        public UpdateVenueNameHandler(IVenuesRepository venuesRepository)
        {
            _venuesRepository = venuesRepository;
        }

        /// <summary>
        /// Метод обновляет имя площадки
        /// </summary>
        /// <returns></returns>
        public async Task<Result<Guid, Error>> Handle(UpdateVenueNameRequest request, CancellationToken cancellationToken)
        {
            // Валидация входных данных
            // Добавить FluentValidation

            // Бизнес валидация

            var venueId = new VenueId(request.Id);
            var venueNameResult = VenueName.CreateWithoutPrefix(request.Name);

            //if (venueNameResult.IsFailure)
            //{
            //    return venueNameResult.Error;
            //}

            //var venueName = venueNameResult.Value;

            var venueResult = await _venuesRepository.GetByIdAsync(venueId, cancellationToken);

            if (venueResult.IsFailure)
            {
                return venueResult.Error;
            }

            var venue = venueResult.Value;
            venue.UpdateNameWithoutPrefix(request.Name);

            await _venuesRepository.SaveChangesAsync(cancellationToken);

            return venueId.Value;
        }        
    }
}
