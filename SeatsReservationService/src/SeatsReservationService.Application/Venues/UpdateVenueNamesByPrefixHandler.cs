using CSharpFunctionalExtensions;
using SeatsReservationService.Application.DataBase;
using SeatsReservationService.Contracts.Venues;
using SeatsReservationService.Domain.Venues;
using Shared;

namespace SeatsReservationService.Application.Venues
{
    public class UpdateVenueNamesByPrefixHandler
    {
        private readonly IVenuesRepository _venuesRepository;

        public UpdateVenueNamesByPrefixHandler(IVenuesRepository venuesRepository)
        {
            _venuesRepository = venuesRepository;
        }

        /// <summary>
        /// Метод обновляет имя площадки
        /// </summary>
        /// <returns></returns>
        public async Task<UnitResult<Error>> Handle(UpdateVenuesNameRequest request, CancellationToken cancellationToken)
        {
            // Валидация входных данных
            // Добавить FluentValidation

            // Бизнес валидация

            var venues = await _venuesRepository.GetByPrefixAsync(request.Prefix, cancellationToken);

            foreach (Venue venue in venues)
            {
                venue.UpdateNameWithoutPrefix(request.Name);
            }

            await _venuesRepository.SaveChangesAsync(cancellationToken);

            return UnitResult.Success<Error>();
        }        
    }
}
