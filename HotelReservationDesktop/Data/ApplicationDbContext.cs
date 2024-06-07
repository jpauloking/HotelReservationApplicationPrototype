using HotelReservationDesktop.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationDesktop.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    public DbSet<ReservationDTO> Reservations { get; set; } = default!;
}
