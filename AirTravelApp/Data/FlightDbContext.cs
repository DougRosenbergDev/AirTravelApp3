using Microsoft.EntityFrameworkCore;
using AirTravelApp.Models;

namespace AirTravelApp.Data
{
    public class FlightDbContext : DbContext
    {
        public FlightDbContext(DbContextOptions<FlightDbContext> options) : base(options)
        {

        }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Booking> BookedFlights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }

        // join entities could be in a separate folder (BookedFlight, DreamFlight, PurchasedFlight)

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Booking>()
                // => big arrow is a Lambda function (anonymous)
                // represents the entity that is being passed in
                .HasKey(b => b.Id);

            builder.Entity<Booking>()
                .HasOne(b => b.Flight)
                .WithMany(b => b.AppearsOnFlights)
                .HasForeignKey(bf => bf.FlightId);
               
            builder.Entity<Booking>()
                .HasOne(bf => bf.Passenger)
                .WithMany(f => f.Bookings)
                .HasForeignKey(bf => bf.PassengerId);

        }
    }
}
