using System;

namespace AspNetCoreApiStarter.Model
{
    public interface IReservation
    {
        int Id { get; }

        string Username { get; }

        int RoomId { get; }

        DateTime Date { get; }

        int Slot { get; }
    }
}
