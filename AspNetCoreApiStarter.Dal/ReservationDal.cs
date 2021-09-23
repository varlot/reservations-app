using AspNetCoreApiStarter.Bll.Itf.Dal;
using AspNetCoreApiStarter.Dal.Base;
using AspNetCoreApiStarter.Dal.Contexte;
using AspNetCoreApiStarter.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApiStarter.Dal
{
    public class ReservationDal : BaseDal, IReservationDal
    {
        private readonly ApiContexte context;

        public ReservationDal()
        {
            var options = new DbContextOptionsBuilder<ApiContexte>()
                .UseInMemoryDatabase(databaseName: "database")
                .Options;
            this.context = new ApiContexte(options);
        }

        public async Task<IEnumerable<Reservation>> Get() => await this.context.Reservations.ToArrayAsync();

        public async Task<Reservation> Get(int id)
        {
            return await this.context.Reservations.FirstOrDefaultAsync(reservation => reservation.Id == id);
        }

        public async Task<Reservation> Get(int roomId, DateTime date, int slot)
        {
            return await this.context.Reservations.FirstOrDefaultAsync(reservation => reservation.RoomId == roomId && reservation.Date == date && reservation.Slot == slot);
        }

        public async Task<Reservation> Create(Reservation reservation)
        {
            this.context.Reservations.Add(reservation);

            int newId = this.context.SaveChanges();
            Reservation newReservation = await this.context.Reservations.FirstOrDefaultAsync(resa => resa.Username == reservation.Username && resa.RoomId == reservation.RoomId && resa.Slot == reservation.Slot && resa.Date == reservation.Date);
            return newReservation;
        }

        public async Task<bool> Delete(int id)
        {
            Reservation reservation = await this.context.Reservations.FirstOrDefaultAsync(reservation => reservation.Id == id);
            this.context.Reservations.Remove(reservation);
            this.context.SaveChanges();
            return true;
        }
    }
}
