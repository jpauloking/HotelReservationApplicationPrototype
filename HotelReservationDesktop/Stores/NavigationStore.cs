using HotelReservationDesktop.ViewModels;

namespace HotelReservationDesktop.Stores;

public class NavigationStore
{

	private ViewModelBase currentViewViewModel;
	public event Action CurrentViewViewModelChangedEvent;

    public ViewModelBase CurrentViewViewModel
	{
		get { return currentViewViewModel; }
		set { 
			// Dispose previous viewmodel if not null
			currentViewViewModel?.Dispose();
			currentViewViewModel = value;
			OnCurrentViewViewModelChanged();
		}
	}

    private void OnCurrentViewViewModelChanged()
    {
        CurrentViewViewModelChangedEvent?.Invoke();
    }
}
