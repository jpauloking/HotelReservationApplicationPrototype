using HotelReservationDesktop.Exceptions;
using HotelReservationDesktop.Models;
using HotelReservationDesktop.Services;
using HotelReservationDesktop.Stores;
using HotelReservationDesktop.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace HotelReservationDesktop.Commands;

public class CreateReservationCommand : AsyncCommandBase
{
    private readonly HotelStore hotelStore;
    private readonly MakeReservationViewModel viewModel;
    private readonly NavigationService<ListReservationsViewModel> navigationService;

    public CreateReservationCommand(HotelStore hotelStore, MakeReservationViewModel viewModel, NavigationService<ListReservationsViewModel> navigationService)
    {
        this.viewModel = viewModel;
        this.navigationService = navigationService;
        this.hotelStore = hotelStore;
        this.viewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(viewModel.Username))
        {
            RaiseCanExecuteChanged();
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return !string.IsNullOrWhiteSpace(viewModel.Username) && base.CanExecute(parameter);
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        RoomId roomId = new RoomId(viewModel.FloorNumber, viewModel.RoomNumber);
        Reservation reservation = new Reservation(roomId, viewModel.Username, viewModel.CheckIn, viewModel.CheckOut);
        try
        {
            await hotelStore.CreateReservationsInDatabase(reservation);
            MessageBox.Show("Room has been reserved.", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
            navigationService.Navigate();
        }
        catch (ReservationConflictException)
        {
            MessageBox.Show("Room is already taken. Please select a vacant room", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch(Exception)
        {
            MessageBox.Show("Failed to make reservation. Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
