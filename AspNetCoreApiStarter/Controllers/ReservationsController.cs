using AspNetCoreApiStarter.Bll.Itf.Bll;
using AspNetCoreApiStarter.Controllers.Core;
using AspNetCoreApiStarter.Model;
using AspNetCoreApiStarter.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApiStarter.Controllers
{
    /// <summary>
    /// Reservation Controller.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ReservationsController : Controller
    {
        private readonly IReservationBll reservationBll;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationsController"/> class.
        /// </summary>
        /// <param name="reservationBll">Reservation services.</param>
        public ReservationsController(IReservationBll reservationBll)
        {
            this.reservationBll = reservationBll;
        }

        /// <summary>
        /// Gets all reservations.
        /// </summary>
        /// <returns>Reservation dto.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponse(typeof(ReservationVm))]
        public async Task<ActionResult<IEnumerable<ReservationVm>>> Get()
        {
            var reservations = await this.reservationBll.Get();
            IEnumerable<ReservationVm> reservationsVm = reservations.Select(reservation => ReservationVm.Load(reservation)).ToList();
            return this.Ok(reservationsVm);
        }

        /// <summary>
        /// Gets a specific reservation.
        /// </summary>
        /// <param name="id">Reservation Id.</param>
        /// <returns>Reservation DTO. Http 200 if Ok.</returns>
        [HttpGet("{id}", Name = "GetReservation")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponse(typeof(ReservationVm))]
        public async Task<ActionResult<ReservationVm>> Get(int id)
        {
            Reservation reservation = await this.reservationBll.Get(id);
            if (reservation == null)
            {
                return this.NotFound();
            }

            return this.Ok(ReservationVm.Load(reservation));
        }

        /// <summary>
        /// Creates a new Reservation.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Reservation
        ///     {
        ///        "userId": "test@test.com",
        ///        "roomId": "password",
        ///        "date": "test",
        ///        "slot": "1"
        ///     }.
        ///
        /// </remarks>
        /// <param name="vm">Reservation VM.</param>
        /// <returns>A newly-created Reservation.</returns>
        /// <response code="201">Returns the newly-created item.</response>
        /// <response code="400">If the item is null.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(typeof(ReservationVm))]
        public async Task<ActionResult<ReservationVm>> Post([FromBody] ReservationVm vm)
        {
            if (vm == null)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return new ValidationFailedResult(this.ModelState);
            }

            Reservation newReservation = await this.reservationBll.Create(ReservationVm.Get(vm));
            ReservationVm newReservationVm = ReservationVm.Load(newReservation);

            return this.CreatedAtRoute("GetReservation", new { id = newReservationVm.Id }, newReservationVm);
        }

        /// <summary>
        /// Deletes a specific reservation.
        /// </summary>
        /// <param name="id">Reservation id.</param>
        /// <returns>Http 200 if ok.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool ok = await this.reservationBll.Delete(id);
            if (!ok)
            {
                return this.NotFound();
            }

            return this.Ok();
        }
    }
}
