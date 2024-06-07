using HotelReservationDesktop.Models;

namespace HotelReservationDesktop.Stores;

public class HotelStore
{
    private readonly List<Reservation> reservations;
    private readonly Hotel hotel;
    private Lazy<Task> reservationsLazy;

    public IEnumerable<Reservation> Reservations => reservations;
    public event Action<Reservation> ReservationCreated = default!;

    public HotelStore(Hotel hotel)
    {
        this.hotel = hotel;
        reservations = new List<Reservation>();
        reservationsLazy = new Lazy<Task>(InitializeReservations);
    }

    public async Task LoadReservationsFromDatabase()
    {
        try
        {
            await reservationsLazy.Value;
        }
        catch (Exception)
        {
            reservationsLazy = new Lazy<Task>(InitializeReservations);
            throw;
        }
    }

    public async Task CreateReservationsInDatabase(Reservation reservation)
    {
        await hotel.MakeReservation(reservation);
        reservations.Add(reservation);
        OnReservationCreated(reservation);
    }

    private void OnReservationCreated(Reservation reservation)
    {
        ReservationCreated?.Invoke(reservation);
    }

    private async Task InitializeReservations()
    {
        IEnumerable<Reservation> reservationsFromDatabase = await hotel.GetReservations();
        reservations.Clear();
        reservations.AddRange(reservationsFromDatabase);
    }
}
