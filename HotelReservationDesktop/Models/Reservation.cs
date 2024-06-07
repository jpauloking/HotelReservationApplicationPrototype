using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationDesktop.Models;

public class Reservation
{
    public RoomId RoomId { get; }
    public string Username { get; }
    public DateTime CheckIn { get; }
    public DateTime CheckOut { get; }
    public TimeSpan Duration => CheckOut.Subtract(CheckIn);

    public Reservation(RoomId roomId, string username, DateTime checkIn, DateTime checkOut)
    {
        RoomId = roomId;
        Username = username;
        CheckIn = checkIn;
        CheckOut = checkOut;
    }

    public bool Conflicts(Reservation reservation)
    {
        bool isConflict = true;
        if (RoomId != reservation.RoomId)
        {
            isConflict = false;
        }

        bool isCheckInBeforeCheckOutConflict = reservation.CheckIn < CheckOut;
        bool isCheckOutAfterCheckInConflict = reservation.CheckOut > CheckIn;
        if (!isCheckInBeforeCheckOutConflict || !isCheckOutAfterCheckInConflict)
        {
            isConflict = false;
        }

        return isConflict;
    }
}
