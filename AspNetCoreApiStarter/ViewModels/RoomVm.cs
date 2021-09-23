using AspNetCoreApiStarter.Model;
using AspNetCoreApiStarter.ViewModels.Core;
using System.Text.Json.Serialization;

namespace AspNetCoreApiStarter.ViewModels
{
    /// <summary>
    /// Defines room view model.
    /// </summary>
    public class RoomVm : BaseVm
    {
        /// <summary>
        /// Gets or sets the id of room.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of room.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        #region Load

        /// <summary>
        /// From model to DTO.
        /// </summary>
        /// <param name="room">Room model.</param>
        /// <returns>Room DTO.</returns>
        public static RoomVm Load(Room room)
        {
            return new RoomVm
            {
                Id = room.Id,
                Name = room.Name,
            };
        }
        #endregion

        #region Get

        /// <summary>
        /// From vm to business object.
        /// </summary>
        /// <param name="vm">Room vm.</param>
        /// <returns>Room business object.</returns>
        public static Room Get(RoomVm vm)
        {
            return new Room
            {
                Id = vm.Id,
                Name = vm.Name,
            };
        }
        #endregion
    }
}
