using AspNetCoreApiStarter.Model;
using AspNetCoreApiStarter.ViewModels.Core;
using System;
using System.Text.Json.Serialization;

namespace AspNetCoreApiStarter.ViewModels
{
    /// <summary>
    /// Defines reservation view model.
    /// </summary>
    public class ReservationVm : BaseVm
    {
        /// <summary>
        /// Gets or sets the id of reservation.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user id of reservation.
        /// </summary>
        [JsonPropertyName("username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the room id of reservation.
        /// </summary>
        [JsonPropertyName("roomId")]
        public int RoomId { get; set; }

        /// <summary>
        /// Gets or sets the date of reservation.
        /// </summary>
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the slot of reservation.
        /// </summary>
        [JsonPropertyName("slot")]
        public int Slot { get; set; }

        #region Load

        /// <summary>
        /// From model to DTO.
        /// </summary>
        /// <param name="reservation">Reservation model.</param>
        /// <returns>Reservation DTO.</returns>
        public static ReservationVm Load(Reservation reservation)
        {
            return new ReservationVm
            {
                Id = reservation.Id,
                Username = reservation.Username,
                RoomId = reservation.RoomId,
                Date = reservation.Date,
                Slot = reservation.Slot
            };
        }
        #endregion

        #region Get

        /// <summary>
        /// From vm to business object.
        /// </summary>
        /// <param name="vm">User vm.</param>
        /// <returns>User business object.</returns>
        public static Reservation Get(ReservationVm vm)
        {
            return new Reservation
            {
                Id = vm.Id,
                Username = vm.Username,
                RoomId = vm.RoomId,
                Date = vm.Date,
                Slot = vm.Slot
            };
        }
        #endregion
    }
}
