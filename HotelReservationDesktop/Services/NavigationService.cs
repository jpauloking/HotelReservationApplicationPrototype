using HotelReservationDesktop.Stores;
using HotelReservationDesktop.ViewModels;

namespace HotelReservationDesktop.Services;

public class NavigationService<TViewModel> where TViewModel : ViewModelBase
{
    private readonly NavigationStore navigationStore;
    private readonly Func<TViewModel> CreateCurrentViewViewModel;

    public NavigationService(NavigationStore navigationStore, Func<TViewModel> createCurrentViewViewModel)
    {
        this.navigationStore = navigationStore;
        CreateCurrentViewViewModel = createCurrentViewViewModel;
    }

    public void Navigate()
    {
        navigationStore.CurrentViewViewModel = CreateCurrentViewViewModel();
    }
}
