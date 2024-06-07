using HotelReservationDesktop.Stores;

namespace HotelReservationDesktop.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly NavigationStore navigationStore;
    public ViewModelBase CurrentViewViewModel => navigationStore.CurrentViewViewModel;

    public MainViewModel(NavigationStore navigationStore)
    {
        this.navigationStore = navigationStore;
        this.navigationStore.CurrentViewViewModelChangedEvent += NavigationStore_CurrentViewViewModelChangedEvent;
    }

    private void NavigationStore_CurrentViewViewModelChangedEvent()
    {
        OnPropertyChanged(nameof(CurrentViewViewModel));
    }
}
