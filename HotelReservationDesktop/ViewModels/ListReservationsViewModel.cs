using HotelReservationDesktop.Commands;
using HotelReservationDesktop.Models;
using HotelReservationDesktop.Services;
using HotelReservationDesktop.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HotelReservationDesktop.ViewModels;

public class ListReservationsViewModel : ViewModelBase
{

    #region Fields
    private readonly ObservableCollection<ReservationViewModel> reservations = [];
    private readonly HotelStore hotelStore;
    #endregion

    #region Properties
    public IEnumerable<ReservationViewModel> Reservations => reservations;
    public bool HasMessage => !string.IsNullOrEmpty(Message);

    private bool isBusy;

    public bool IsBusy
    {
        get { return isBusy; }
        set { 
            isBusy = value;
            OnPropertyChanged();
        }
    }

    private string? message;

    public string? Message
    {
        get { return message; }
        set
        {
            message = value;
            OnPropertyChanged(nameof(Message));
            OnPropertyChanged(nameof(HasMessage));
        }
    }
    #endregion

    #region Commands
    public ICommand ListReservationsCommand { get; }
    public ICommand MakeReservationCommand { get; }
    #endregion

    #region Constructors
    public ListReservationsViewModel(HotelStore hotelStore, NavigationService<MakeReservationViewModel> navigationService)
    {
        this.hotelStore = hotelStore;
        ListReservationsCommand = new ListReservationsCommand(hotelStore, this);
        MakeReservationCommand = new NavigateCommand<MakeReservationViewModel>(navigationService);
        hotelStore.ReservationCreated += HotelStore_ReservationCreated;
    }
    #endregion

    #region Methods
    public static ListReservationsViewModel CreateViewModel(HotelStore hotelStore, NavigationService<MakeReservationViewModel> navigationService)
    {
        ListReservationsViewModel viewModel = new ListReservationsViewModel(hotelStore, navigationService);
        viewModel.ListReservationsCommand.Execute(null);
        return viewModel;
    }

    public void UpdateReservations(IEnumerable<Reservation> reservationsFromDatabase)
    {
        foreach (var reservation in reservationsFromDatabase)
        {
            ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
            reservations.Add(reservationViewModel);
        }
    }

    public override void Dispose()
    {
        hotelStore.ReservationCreated -= HotelStore_ReservationCreated;
        base.Dispose();
    }

    private void HotelStore_ReservationCreated(Reservation createdReservation)
    {
        ReservationViewModel reservationViewModel = new ReservationViewModel(createdReservation);
        reservations.Add(reservationViewModel);
    }
    #endregion

}
