using CSharpFunctionalExtensions;
using Shared;

namespace SeatsReservationService.Application.DataBase
{
    public interface ITransactionManager
    {
        public Task<Result<ITransactionScope, Error>> BeginTransactionAsync(CancellationToken cancellationToken);
        public Task<UnitResult<Error>> SaveChangesAsync(CancellationToken cancellationToken);
    }
}