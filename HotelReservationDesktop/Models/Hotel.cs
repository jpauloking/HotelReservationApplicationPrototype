using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationDesktop.Models;

public class Hotel
{
    public string Name { get; }
    private readonly ReservationBook reservationBook;

    public Hotel(string name, ReservationBook reservationBook)
    {
        this.Name = name;
        this.reservationBook = reservationBook;
    }

    public async Task<IEnumerable<Reservation>> GetReservations() => await reservationBook.GetReservations();

    public async Task MakeReservation(Reservation reservation) => await reservationBook.AddReservation(reservation);

}
