using HotelReservationDesktop.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HotelReservationDesktop.Extensions.Host;

public static class StoreExtensions
{
    public static IHostBuilder AddStores(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices(services =>
        {
            services.AddSingleton<HotelStore>();
            services.AddSingleton<NavigationStore>();
        });
        return hostBuilder;
    }
}
