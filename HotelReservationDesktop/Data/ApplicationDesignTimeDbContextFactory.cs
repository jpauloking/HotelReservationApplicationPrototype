using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HotelReservationDesktop.Data;

public class ApplicationDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite("Data Source=Application.db");
        DbContextOptions<ApplicationDbContext> dbContextOptions = optionsBuilder.Options;

        return new ApplicationDbContext(dbContextOptions);
    }
}
