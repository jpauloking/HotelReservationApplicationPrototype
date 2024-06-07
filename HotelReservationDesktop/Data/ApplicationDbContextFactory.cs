using Microsoft.EntityFrameworkCore;

namespace HotelReservationDesktop.Data;

public class ApplicationDbContextFactory
{
    private readonly string connectionString;

    public ApplicationDbContextFactory(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public ApplicationDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(connectionString);
        DbContextOptions<ApplicationDbContext> dbContextOptions = optionsBuilder.Options;

        return new ApplicationDbContext(dbContextOptions);
    }
}
