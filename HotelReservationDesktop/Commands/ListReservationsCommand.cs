
using HotelReservationDesktop.Models;
using HotelReservationDesktop.Stores;
using HotelReservationDesktop.ViewModels;

namespace HotelReservationDesktop.Commands;

public class ListReservationsCommand : AsyncCommandBase
{
    private readonly HotelStore hotelStore;
    private readonly ListReservationsViewModel viewModel;

    public ListReservationsCommand(HotelStore hotelStore, ListReservationsViewModel viewModel)
    {
        this.hotelStore = hotelStore;
        this.viewModel = viewModel;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            viewModel.IsBusy = true;
            viewModel.Message = string.Empty;
            await hotelStore.LoadReservationsFromDatabase();
            IEnumerable<Reservation> reservations = hotelStore.Reservations;
            viewModel.UpdateReservations(reservations);
        }
        catch(Exception)
        {
            viewModel.Message = "Failed to load reservations. Please try again";
        }
        finally
        {
            viewModel.IsBusy = false;
        }
    }
}
