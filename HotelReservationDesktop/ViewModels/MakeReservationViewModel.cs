using HotelReservationDesktop.Commands;
using HotelReservationDesktop.Services;
using HotelReservationDesktop.Stores;
using System.Windows.Input;

namespace HotelReservationDesktop.ViewModels;

public class MakeReservationViewModel : ViewModelBase
{

    #region Fields
	private static DateTime today = DateTime.Today;
    #endregion

    #region Properties
    private string username = default!;

	public string Username
	{
		get { return username; }
		set { 
			username = value;
			OnPropertyChanged();
		}
	}

	private int floorNumber;

	public int FloorNumber
	{
		get { return floorNumber; }
		set { 
			floorNumber = value;
            OnPropertyChanged();
        }
	}

	private int roomNumber;

	public int RoomNumber
	{
		get { return roomNumber; }
		set { 
			roomNumber = value;
            OnPropertyChanged();
        }
	}

	private DateTime checkIn = today;

	public DateTime CheckIn
	{
		get { return checkIn; }
		set { 
			checkIn = value;
            OnPropertyChanged();
        }
	}

	private DateTime checkOut = today.AddDays(7);

	public DateTime CheckOut
	{
		get { return checkOut; }
		set { 
			checkOut = value;
            OnPropertyChanged();
        }
	}
    #endregion

    #region Commands
	public ICommand CreateCommand { get; }
	public ICommand CancelCommand { get; }
    #endregion

    #region Constructors
    public MakeReservationViewModel(HotelStore hotelStore, NavigationService<ListReservationsViewModel> navigationService)
    {
        CreateCommand = new CreateReservationCommand(hotelStore, this, navigationService);
		CancelCommand = new NavigateCommand<ListReservationsViewModel>(navigationService);
    }
    #endregion
}
