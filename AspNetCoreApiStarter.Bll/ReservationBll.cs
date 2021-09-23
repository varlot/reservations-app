using AspNetCoreApiStarter.Bll.Base;
using AspNetCoreApiStarter.Bll.Itf.Bll;
using AspNetCoreApiStarter.Bll.Itf.Dal;
using AspNetCoreApiStarter.Model;
using AspNetCoreApiStarter.Resources;
using AspNetCoreApiStarter.Shared;
using AspNetCoreApiStarter.Shared.Logger;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApiStarter.Bll
{
    /// <summary>
    /// Reservation Bll.
    /// </summary>
    public class ReservationBll : BaseBll, IReservationBll
    {
        private readonly IReservationDal reservationDal;

        #region Constructor
        /// <summary>
        /// Creates an instance of <see cref="ReservationBll" />.
        /// </summary>
        /// <param name="connConfig"></param>
        /// <param name="logger"></param>
        /// <param name="localizer"></param>
        public ReservationBll(
            IReservationDal reservationDal,
            IOptions<AppConfig> config,
            ILoggerHelper<ReservationBll> logger,
            IStringLocalizer<SharedResources> localizer
        ) {
            this.reservationDal = reservationDal;
            this.AppConfig = config.Value;
            this.Localizer = localizer;
            this.Logger = logger;
        }
        #endregion Constructor

        #region Get
        /// <summary>
        /// Gets all reservations
        /// </summary>
        /// <returns>Reservations list</returns>
        public async Task<IEnumerable<Reservation>> Get()
        {
            return await this.reservationDal.Get();
        }

        /// <summary>
        /// Gets a specific reservation
        /// </summary>
        /// <param name="id">Reservation id</param>
        /// <returns>The Reservation</returns>
        public async Task<Reservation> Get(int id)
        {
            return await this.reservationDal.Get(id);
        }

        /// <summary>
        /// Gets a specific reservation
        /// </summary>
        /// <param name="roomId">Room id</param>
        /// <param name="date">Date</param>
        /// <param name="slot">Slot</param>
        /// <returns>The Reservation</returns>
        public async Task<Reservation> Get(int roomId, DateTime date, int slot)
        {
            return await this.reservationDal.Get(roomId, date, slot);
        }
        #endregion Get

        #region Create
        /// <summary>
        /// Creates a reservation
        /// </summary>
        /// <param name="reservation">Reservation to create.</param>
        /// <returns>Reservation created</returns>
        public async Task<Reservation> Create(Reservation reservation)
        {
            Reservation newReservation = await this.reservationDal.Create(reservation);
            return newReservation;
        }
        #endregion Create

        #region Delete
        /// <summary>
        /// Deletes a specific Reservation
        /// </summary>
        /// <param name="id">Reservation id to delete</param>
        public async Task<bool> Delete(int id)
        {
             return await this.reservationDal.Delete(id);
        }
        #endregion Delete
    }
}