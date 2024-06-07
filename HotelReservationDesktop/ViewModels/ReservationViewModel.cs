using HotelReservationDesktop.Models;

namespace HotelReservationDesktop.ViewModels;

public class ReservationViewModel : ViewModelBase
{
    private readonly Reservation reservation;

    public ReservationViewModel(Reservation reservation)
    {
        this.reservation = reservation;
    }

    public string RoomId => reservation.RoomId.ToString();
    public string Username => reservation.Username;
    public DateTime CheckIn => reservation.CheckIn;
    public DateTime CheckOut => reservation.CheckOut;

}
