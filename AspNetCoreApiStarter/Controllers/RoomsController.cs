using AspNetCoreApiStarter.Bll.Itf.Bll;
using AspNetCoreApiStarter.Model;
using AspNetCoreApiStarter.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApiStarter.Controllers
{
    /// <summary>
    /// Room Controller.
    /// </summary>
    // [Authorize(Policy = "ApiRoom")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RoomsController : Controller
    {
        private readonly IRoomBll roomBll;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomsController"/> class.
        /// </summary>
        /// <param name="roomBll">Room services.</param>
        public RoomsController(IRoomBll roomBll)
        {
            this.roomBll = roomBll;
        }

        /// <summary>
        /// Gets all rooms.
        /// </summary>
        /// <returns>Room dto.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomVm>>> Get()
        {
            var rooms = await this.roomBll.Get();
            IEnumerable<RoomVm> roomsVm = rooms.Select(room => RoomVm.Load(room)).ToList();
            return this.Ok(roomsVm);
        }

        /// <summary>
        /// Gets a specific room.
        /// </summary>
        /// <param name="id">Room Id.</param>
        /// <returns>room DTO. Http 200 if Ok.</returns>
        [HttpGet("{id}", Name = "GetRoom")]
        public async Task<ActionResult<RoomVm>> Get(int id)
        {
            Room room = await this.roomBll.Get(id);
            if (room == null)
            {
                return this.NotFound();
            }

            return this.Ok(RoomVm.Load(room));
        }
    }
}
