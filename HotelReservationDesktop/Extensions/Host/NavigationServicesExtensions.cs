using HotelReservationDesktop.Services;
using HotelReservationDesktop.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HotelReservationDesktop.Extensions.Host;

public static class NavigationServicesExtensions
{
    public static IHostBuilder AddNavigationServices(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices(services =>
        {
            services.AddSingleton<NavigationService<ListReservationsViewModel>>();
            services.AddSingleton<NavigationService<MakeReservationViewModel>>();
        });
        return hostBuilder;
    }
}
