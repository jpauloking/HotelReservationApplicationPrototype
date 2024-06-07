using HotelReservationDesktop.Data;
using HotelReservationDesktop.DTOs;
using HotelReservationDesktop.Models;

namespace HotelReservationDesktop.Services;

public class CreateReservationService : ICreateReservationService
{
    private readonly ApplicationDbContextFactory applicationDbContextFactory;

    public CreateReservationService(ApplicationDbContextFactory applicationDbContextFactory)
    {
        this.applicationDbContextFactory = applicationDbContextFactory;
    }

    public async Task CreateReservation(Reservation reservation)
    {
        using ApplicationDbContext context = applicationDbContextFactory.CreateDbContext();
        ReservationDTO reservationDTO = ReservationDTOFromReservation(reservation);
        context.Reservations.Add(reservationDTO);
        await context.SaveChangesAsync();
    }

    private ReservationDTO ReservationDTOFromReservation(Reservation reservation)
    {
        return new ReservationDTO()
        {
            FloorNumber = reservation.RoomId.FloorNumber,
            RoomNumber = reservation.RoomId.RoomNumber,
            Username = reservation.Username,
            CheckIn = reservation.CheckIn,
            CheckOut = reservation.CheckOut,
        };
    }
}
