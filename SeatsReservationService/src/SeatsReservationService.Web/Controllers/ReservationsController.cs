using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeatsReservationService.Application.Reservations;
using SeatsReservationService.Contracts.Reservation;

namespace SeatsReservationService.Web.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationsController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Reserve(
            [FromServices] CreateReservationHandler handler,
            [FromBody] CreateReservationRequest request,
            CancellationToken cancellationToken)
        {
            var handlerResult = await handler.Handle(request, cancellationToken);

            return handlerResult.IsFailure ? BadRequest(handlerResult.Error) : Ok(handlerResult.Value);
        }
    }
}
