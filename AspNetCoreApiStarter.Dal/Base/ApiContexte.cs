using AspNetCoreApiStarter.Model;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreApiStarter.Dal.Contexte
{
    public class ApiContexte : DbContext
    {
        public ApiContexte(DbContextOptions<ApiContexte> options)
    : base(options)
        {
        }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Room> Rooms { get; set; }
    }
}