using AspNetCoreApiStarter.Bll.Base;
using AspNetCoreApiStarter.Bll.Itf.Bll;
using AspNetCoreApiStarter.Bll.Itf.Dal;
using AspNetCoreApiStarter.Model;
using AspNetCoreApiStarter.Resources;
using AspNetCoreApiStarter.Shared;
using AspNetCoreApiStarter.Shared.Logger;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApiStarter.Bll
{
    /// <summary>
    /// Room Bll.
    /// </summary>
    public class RoomBll : BaseBll, IRoomBll
    {
        private readonly IRoomDal roomDal;

        #region Constructor
        /// <summary>
        /// Creates an instance of <see cref="RoomBll" />.
        /// </summary>
        /// <param name="connConfig"></param>
        /// <param name="logger"></param>
        /// <param name="localizer"></param>
        public RoomBll(
            IRoomDal roomDal,
            IOptions<AppConfig> config,
            ILoggerHelper<RoomBll> logger,
            IStringLocalizer<SharedResources> localizer
        ) {
            this.roomDal = roomDal;
            this.AppConfig = config.Value;
            this.Localizer = localizer;
            this.Logger = logger;
        }
        #endregion Constructor

        #region Load
        /// <summary>
        /// Load data in database.
        /// </summary>
        /// <returns>True if data loaded.</returns>
        public async Task<bool> LoadData()
        {
            return await this.roomDal.LoadData();
        }
        #endregion

        #region Get
        /// <summary>
        /// Gets all rooms.
        /// </summary>
        /// <returns>Rooms list</returns>
        public async Task<IEnumerable<Room>> Get()
        {
            return await this.roomDal.Get();
        }

        /// <summary>
        /// Gets a specific room.
        /// </summary>
        /// <param name="id">Room id</param>
        /// <returns>The Room</returns>
        public async Task<Room> Get(int id)
        {
            return await this.roomDal.Get(id);
        }

        /// <summary>
        /// Gets a specific room.
        /// </summary>
        /// <param name="name">Room name</param>
        /// <returns>The Room</returns>
        public async Task<Room> Get(string name)
        {
            return await this.roomDal.Get(name);
        }
        #endregion Get
    }
}