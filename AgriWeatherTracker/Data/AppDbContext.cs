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
        public DbSet<Location> Locations { get; set; }
        public DbSet<Weather> Weathers { get; set; }
        public DbSet<GrowthCycle> GrowthCycles { get; set; }
        public DbSet<GrowthStage> GrowthStages { get; set; }
        public DbSet<CropLocation> CropLocations { get; set; }
        public DbSet<ConditionThreshold> ConditionThresholds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring the many-to-many relationship between Crops and Locations
            modelBuilder.Entity<CropLocation>()
                .HasKey(cl => new { cl.CropId, cl.LocationId });

            modelBuilder.Entity<CropLocation>()
                .HasOne(cl => cl.Crop)
                .WithMany(c => c.CropLocations)
                .HasForeignKey(cl => cl.CropId);

            modelBuilder.Entity<CropLocation>()
                .HasOne(cl => cl.Location)
                .WithMany(l => l.CropLocations)
                .HasForeignKey(cl => cl.LocationId);

            // Optional: Additional configurations for other relationships
            // This could include unique constraints, indexes, or other entity configurations
        }
    }
}
