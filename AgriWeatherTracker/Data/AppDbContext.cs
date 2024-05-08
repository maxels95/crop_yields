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
        public DbSet<ConditionThreshold> ConditionThresholds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the one-to-many relationship between Crop and Location
            modelBuilder.Entity<Crop>()
                .HasMany(c => c.Locations)
                .WithOne(l => l.Crop)
                .HasForeignKey(l => l.CropId);
                
            modelBuilder.Entity<Location>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Location>()
                .HasOne(l => l.Crop)
                .WithMany(c => c.Locations)
                .HasForeignKey(l => l.CropId)
                .IsRequired(false);  // Indicates that the foreign key is not required

            modelBuilder.Entity<GrowthStage>()
                .Property(e => e.StartDate)
                .HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            modelBuilder.Entity<GrowthStage>()
                .Property(e => e.EndDate)
                .HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            modelBuilder.Entity<ConditionThreshold>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<GrowthStage>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
