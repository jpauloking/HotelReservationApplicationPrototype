using HotelReservationDesktop.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HotelReservationDesktop.Extensions.Host;

public static class ReservationServicesExtensions
{
    public static IHostBuilder AddReservationServices(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices(services =>
        {
            services.AddSingleton<IListReservationsService, ListReservationsService>();
            services.AddSingleton<ICreateReservationService, CreateReservationService>();
            services.AddSingleton<IReservationConflictValidatorService, ReservationConflictValidatorService>();
        });
        return hostBuilder;
    }
}
