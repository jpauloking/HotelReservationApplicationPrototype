using HotelReservationDesktop.Data;
using HotelReservationDesktop.DTOs;
using HotelReservationDesktop.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationDesktop.Services;

public class ReservationConflictValidatorService : IReservationConflictValidatorService
{
    private readonly ApplicationDbContextFactory applicationDbContextFactory;

    public ReservationConflictValidatorService(ApplicationDbContextFactory applicationDbContextFactory)
    {
        this.applicationDbContextFactory = applicationDbContextFactory;
    }

    public async Task<Reservation?> GetConflictingReservationIfExists(Reservation reservation)
    {
        using ApplicationDbContext context = applicationDbContextFactory.CreateDbContext();
        ReservationDTO? conflictingReservationDTO = await context.Reservations
            .Where(dto => dto.FloorNumber == reservation.RoomId.FloorNumber)
            .Where(dto => dto.RoomNumber == reservation.RoomId.RoomNumber)
            .Where(dto => dto.CheckIn < reservation.CheckOut)
            .Where(dto => dto.CheckOut > reservation.CheckIn)
            .FirstOrDefaultAsync();
        Reservation conflictingReservation = default!;
        if (conflictingReservationDTO is not null)
        {
            conflictingReservation = ReservationFromReservationDTO(conflictingReservationDTO);
        }
        return conflictingReservation;
    }

    //private static Reservation ReservationFromReservationDTO(ReservationDTO dto)
    private Reservation ReservationFromReservationDTO(ReservationDTO dto)
    {
        RoomId roomId = new RoomId(dto.FloorNumber, dto.RoomNumber);
        return new Reservation(roomId, dto.Username, dto.CheckIn, dto.CheckOut);
    }
}
