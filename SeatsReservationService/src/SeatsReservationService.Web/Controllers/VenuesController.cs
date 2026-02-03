using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeatsReservationService.Application.Venues;
using SeatsReservationService.Contracts.Venues;

namespace SeatsReservationService.Web.Controllers
{
    [ApiController]
    [Route("api/venues")]
    public class VenuesController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromServices] CreateVenueHandler handler,
            [FromBody] CreateVenueRequest request,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(request, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
    }
}
