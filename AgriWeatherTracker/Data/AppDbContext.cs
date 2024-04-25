using Microsoft.EntityFrameworkCore;
using AgriWeatherTracker.Models;

namespace AgriWeatherTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Crop> Crops { get; set; }
        // Add other DbSet properties for other entities
    }
}
