using AspNetCoreApiStarter.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApiStarter.Bll.Itf.Dal
{
    public interface IRoomDal : IBaseDb
    {
        /// <summary>
        /// Gets all rooms.
        /// </summary>
        /// <returns>Rooms list.</returns>
        Task<IEnumerable<Room>> Get();

        /// <summary>
        /// Gets a specific room by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The Room.</returns>
        Task<Room> Get(int id);

        /// <summary>
        /// Gets a specific room by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The Room.</returns>
        Task<Room> Get(string name);

        /// <summary>
        /// Load data in database.
        /// </summary>
        /// <returns>True if data loaded.</returns>
        Task<bool> LoadData();
    }
}
