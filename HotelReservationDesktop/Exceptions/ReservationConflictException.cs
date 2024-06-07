using HotelReservationDesktop.Models;

namespace HotelReservationDesktop.Exceptions;

public class ReservationConflictException : Exception
{
    public Reservation ReservationInDatabase { get; }
    public Reservation ConflictingReservation { get; }

    public ReservationConflictException(Reservation reservationInDatabase, Reservation conflictingReservation)
    {
        ReservationInDatabase = reservationInDatabase;
        ConflictingReservation = conflictingReservation;
    }

    public ReservationConflictException(string? message, Reservation reservationInDatabase, Reservation conflictingReservation) : base(message)
    {
        ReservationInDatabase = reservationInDatabase;
        ConflictingReservation = conflictingReservation;
    }

    public ReservationConflictException(string? message, Exception? innerException, Reservation reservationInDatabase, Reservation conflictingReservation) : base(message, innerException)
    {
        ReservationInDatabase = reservationInDatabase;
        ConflictingReservation = conflictingReservation;
    }
}
