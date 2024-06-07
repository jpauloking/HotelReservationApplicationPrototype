using HotelReservationDesktop.Data;
using HotelReservationDesktop.Models;
using HotelReservationDesktop.Services;
using HotelReservationDesktop.ViewModels;
using HotelReservationDesktop.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using HotelReservationDesktop.Extensions.Host;

namespace HotelReservationDesktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private string ConnectionString = null!;
    private readonly IHost applicationHost;

    public App()
    {
        applicationHost = Host.CreateDefaultBuilder()
            .AddReservationServices()
            .AddNavigationServices()
            .AddStores()
            .AddViewModels()
            .ConfigureServices((builder, services) =>
            {
                string hotelName = builder.Configuration.GetSection("Hotel")["Name"]!;
                ConnectionString = builder.Configuration.GetConnectionString("Default")!;
                services.AddSingleton(new ApplicationDbContextFactory(ConnectionString));
                services.AddTransient<ReservationBook>();
                services.AddSingleton(s => new Hotel(hotelName, s.GetRequiredService<ReservationBook>()));
                services.AddSingleton(s => new MainWindow()
                {
                    DataContext = s.GetRequiredService<MainViewModel>()
                });
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        applicationHost.Start();
        RunDatabaseMigrations(applicationHost);

        NavigationService<ListReservationsViewModel> navigationService = applicationHost.Services.GetRequiredService<NavigationService<ListReservationsViewModel>>();
        navigationService.Navigate();

        MainWindow = applicationHost.Services.GetRequiredService<MainWindow>();
        MainWindow.Show();
        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        applicationHost.Dispose();
        base.OnExit(e);
    }

    private void RunDatabaseMigrations(IHost host)
    {
        ApplicationDbContextFactory applicationDbContextFactory = host.Services.GetRequiredService<ApplicationDbContextFactory>();
        using ApplicationDbContext dbContext = applicationDbContextFactory.CreateDbContext();
        dbContext.Database.Migrate();
    }
}
