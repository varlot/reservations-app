using AspNetCoreApiStarter.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApiStarter.Bll.Itf.Dal
{
    public interface IReservationDal : IBaseDb
    {
        /// <summary>
        /// Gets all reservations
        /// </summary>
        /// <returns>Reservations list</returns>
        Task<IEnumerable<Reservation>> Get();

        /// <summary>
        /// Gets a specific reservation
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Reservation</returns>
        Task<Reservation> Get(int id);

        /// <summary>
        /// Gets a specific reservation
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="date"></param>
        /// <param name="slot"></param>
        /// <returns>The Reservation</returns>
        Task<Reservation> Get(int roomId, DateTime date, int slot);

        /// <summary>
        /// Creates a reservation
        /// </summary>
        /// <param name="reservation">Reservation to create</param>
        /// <returns>Created reservation</returns>
        Task<Reservation> Create(Reservation reservation);

        /// <summary>
        /// Deletes a specific reservation
        /// </summary>
        /// <param name="id">Reservation id to delete</param>
        Task<bool> Delete(int id);
    }
}
