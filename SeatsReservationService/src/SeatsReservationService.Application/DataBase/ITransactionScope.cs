using CSharpFunctionalExtensions;
using Shared;

namespace SeatsReservationService.Application.DataBase
{
    public interface ITransactionScope : IDisposable
    {
        public UnitResult<Error> Commit();
        public UnitResult<Error> Rollback();
    }
}