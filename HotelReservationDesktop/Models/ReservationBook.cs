using HotelReservationDesktop.Exceptions;
using HotelReservationDesktop.Services;

namespace HotelReservationDesktop.Models;

public class ReservationBook
{
    private readonly IListReservationsService listReservationsService;
    private readonly ICreateReservationService createReservationService;
    private readonly IReservationConflictValidatorService reservationConflictValidatorService;

    public ReservationBook(IListReservationsService listReservationsService, ICreateReservationService createReservationService, IReservationConflictValidatorService reservationConflictValidatorService)
    {
        this.listReservationsService = listReservationsService;
        this.createReservationService = createReservationService;
        this.reservationConflictValidatorService = reservationConflictValidatorService;
    }

    public async Task<IEnumerable<Reservation>> GetReservations() => await listReservationsService.GetReservations();

    public async Task AddReservation(Reservation reservation)
    {
        Reservation? conflictingReservation = await reservationConflictValidatorService.GetConflictingReservationIfExists(reservation);
        bool hasConflictingReservation = conflictingReservation is not null;
        if (hasConflictingReservation)
        {
            throw new ReservationConflictException(conflictingReservation!, reservation);
        }
        await createReservationService.CreateReservation(reservation);
    }
}
