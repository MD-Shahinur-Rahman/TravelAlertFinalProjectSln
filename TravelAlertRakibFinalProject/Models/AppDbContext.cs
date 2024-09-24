using Microsoft.EntityFrameworkCore;

namespace TravelAlertRakibFinalProject.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options) 
        {
            
        }
        public DbSet<Hotel>  Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<HotelImage> HotelImages { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
    
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<HotelFacility> HotelFacilities { get; set; }
        public DbSet<RoomFacility> RoomFacilities { get; set; }
        public DbSet<Location> Locations { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.HotelFacilities) // Navigation property for HotelFacilities
                .WithOne(f => f.Hotel)
                .HasForeignKey(f => f.HotelId);
            modelBuilder.Entity<Room>()
                .HasMany(h => h.RoomFacilities) // Navigation property for HotelFacilities
                .WithOne(f => f.Room)
                .HasForeignKey(f => f.RoomId);

          

        }

    }
}
