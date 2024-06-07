using HotelReservationDesktop.Data;
using HotelReservationDesktop.DTOs;
using HotelReservationDesktop.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationDesktop.Services;

public class ListReservationsService : IListReservationsService
{
    private readonly ApplicationDbContextFactory applicationDbContextFactory;

    public ListReservationsService(ApplicationDbContextFactory applicationDbContextFactory)
    {
        this.applicationDbContextFactory = applicationDbContextFactory;
    }

    public async Task<IEnumerable<Reservation>> GetReservations()
    {
        using ApplicationDbContext context = applicationDbContextFactory.CreateDbContext();
        List<DTOs.ReservationDTO> reservationFromDatabase = await context.Reservations.ToListAsync();
        return reservationFromDatabase.Select(dto => ReservationFromReservationDTO(dto));
    }

    private Reservation ReservationFromReservationDTO(ReservationDTO dto)
    {
        RoomId roomId = new RoomId(dto.FloorNumber, dto.RoomNumber);
        return new Reservation(roomId, dto.Username, dto.CheckIn, dto.CheckOut);
    }
}
