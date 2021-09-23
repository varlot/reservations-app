using AspNetCoreApiStarter.Bll.Itf.Dal;
using AspNetCoreApiStarter.Dal.Base;
using AspNetCoreApiStarter.Dal.Contexte;
using AspNetCoreApiStarter.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApiStarter.Dal
{
    public class RoomDal : BaseDal, IRoomDal
    {
        private readonly ApiContexte context;

        public RoomDal()
        {
            var options = new DbContextOptionsBuilder<ApiContexte>()
                .UseInMemoryDatabase(databaseName: "database")
                .Options;
            this.context = new ApiContexte(options);
        }

        public async Task<bool> LoadData()
        {
            int insertedRows = 0;
            foreach (int i in Enumerable.Range(1, 10))
            {
                this.context.Rooms.Add(new Room
                {
                    Id = i,
                    Name = $"Room{i}"
                });
                this.context.SaveChanges();
                insertedRows++;
            }

            return insertedRows > 0;
        }

        public async Task<IEnumerable<Room>> Get() => await this.context.Rooms.ToArrayAsync();

        public async Task<Room> Get(int id)
        {
            return await this.context.Rooms.FirstOrDefaultAsync(room => room.Id == id);
        }

        public async Task<Room> Get(string name)
        {
            return await this.context.Rooms.FirstOrDefaultAsync(room => room.Name == name);
        }
    }
}
