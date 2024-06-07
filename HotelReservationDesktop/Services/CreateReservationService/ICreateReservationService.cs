using HotelReservationDesktop.Models;

namespace HotelReservationDesktop.Services
{
    public interface ICreateReservationService
    {
        Task CreateReservation(Reservation reservation);
    }
}