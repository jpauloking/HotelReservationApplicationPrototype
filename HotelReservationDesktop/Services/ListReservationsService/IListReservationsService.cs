using HotelReservationDesktop.Models;

namespace HotelReservationDesktop.Services;

public interface IListReservationsService
{
    Task<IEnumerable<Reservation>> GetReservations();
}