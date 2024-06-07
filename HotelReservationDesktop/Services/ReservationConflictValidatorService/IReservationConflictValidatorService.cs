using HotelReservationDesktop.Models;

namespace HotelReservationDesktop.Services
{
    public interface IReservationConflictValidatorService
    {
        Task<Reservation?> GetConflictingReservationIfExists(Reservation reservation);
    }
}