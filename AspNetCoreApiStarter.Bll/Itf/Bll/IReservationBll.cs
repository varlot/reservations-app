using AspNetCoreApiStarter.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApiStarter.Bll.Itf.Bll
{
    /// <summary>
    /// Reservation Bll Interface.
    /// </summary>
    public interface IReservationBll
    {
        /// <summary>
        /// Gets all reservations.
        /// </summary>
        /// <returns>Reservations list.</returns>
        Task<IEnumerable<Reservation>> Get();

        /// <summary>
        /// Gets a specific reservation by id.
        /// </summary>
        /// <param name="id">Reservation id.</param>
        /// <returns>The Reservation.</returns>
        Task<Reservation> Get(int id);

        /// <summary>
        /// Gets a specific reservation by id.
        /// </summary>
        /// <param name="roomId">Room id</param>
        /// <param name="date">Date</param>
        /// <param name="slot">Slot</param>>
        /// <returns>The Reservation.</returns>
        Task<Reservation> Get(int roomId, DateTime date, int slot);

        /// <summary>
        /// Creates a reservation.
        /// </summary>
        /// <param name="reservation">Reservation to create.</param>
        /// <returns>Created reservation.</returns>
        Task<Reservation> Create(Reservation reservation);

        /// <summary>
        /// Deletes a specific reservation.
        /// </summary>
        /// <param name="id">Reservation id to delete</param>
        /// <returns>True if deleted reservation</returns>
        Task<bool> Delete(int id);
    }
}