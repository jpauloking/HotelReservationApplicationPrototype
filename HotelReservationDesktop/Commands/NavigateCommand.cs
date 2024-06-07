using HotelReservationDesktop.Services;
using HotelReservationDesktop.ViewModels;

namespace HotelReservationDesktop.Commands;

public class NavigateCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
{
    private readonly NavigationService<TViewModel> navigationService;

    public NavigateCommand(NavigationService<TViewModel> navigationService)
    {
        this.navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        navigationService.Navigate();
    }
}
