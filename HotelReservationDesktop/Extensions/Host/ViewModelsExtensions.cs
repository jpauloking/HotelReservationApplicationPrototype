using HotelReservationDesktop.Services;
using HotelReservationDesktop.Stores;
using HotelReservationDesktop.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HotelReservationDesktop.Extensions.Host;

public static class ViewModelsExtensions
{
    public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices(services => {
            services.AddTransient<MakeReservationViewModel>();
            services.AddSingleton<Func<MakeReservationViewModel>>(s => () => s.GetRequiredService<MakeReservationViewModel>());
            services.AddTransient(s => CreateListReservationsViewModel(s));
            services.AddSingleton<Func<ListReservationsViewModel>>(s => () => s.GetRequiredService<ListReservationsViewModel>());
            services.AddSingleton<MainViewModel>();
        });
        return hostBuilder;
    }

    private static ListReservationsViewModel CreateListReservationsViewModel(IServiceProvider s)
    {
        return ListReservationsViewModel.CreateViewModel(
            s.GetRequiredService<HotelStore>(), s.GetRequiredService<NavigationService<MakeReservationViewModel>>());
    }
}
