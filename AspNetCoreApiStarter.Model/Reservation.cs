using System;

namespace AspNetCoreApiStarter.Model
{
    public class Reservation
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public int RoomId { get; set; }

        public DateTime Date { get; set; }

        public int Slot { get; set; }
    }
}
