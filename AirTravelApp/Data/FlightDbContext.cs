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
            builder.Entity<BookedFlight>()
                // => big arrow is a Lambda function (anonymous)
                // represents the entity that is being passed in
                .HasKey(bf => bf.Id);

            builder.Entity<BookedFlight>()
                .HasOne(bf => bf.Booking)
                .WithMany(b => b.Flights)
                .HasForeignKey(bf => bf.BookingId);
               
            builder.Entity<BookedFlight>()
                .HasOne(bf => bf.Flight)
                .WithMany(f => f.AppearsOnFlights)
                .HasForeignKey(bf => bf.FlightId);

            // define fk for DreamFlight
            builder.Entity<DreamFlight>()    
                .HasKey(bf => bf.Id);

            // person to playist relation
            builder.Entity<DreamFlight>()
                .HasOne(df => df.Passenger)
                .WithMany(p => p.DreamFlights)
                .HasForeignKey(df => df.PassengerId);
            // do both sides
            builder.Entity<DreamFlight>()
                .HasOne(df => df.Booking)
                .WithMany(b => b.Dreams)
                .HasForeignKey(df => df.BookingId);

            // define fk for BookedFlight
            builder.Entity<BookedFlight>()    
                .HasKey(bf => bf.Id);

            // owned playlist
            builder.Entity<PurchasedFlight>()
                .HasOne(pf => pf.Passenger)
                .WithMany(p => p.PurchasedFlights)
                .HasForeignKey(pf => pf.PassengerId);
            // do both sides
            builder.Entity<PurchasedFlight>()
                .HasOne(pf => pf.Booking)
                .WithMany(b => b.Purchasers)
                .HasForeignKey(pf => pf.BookingId);

            // could be condensed in a different way: 2:30
            // e.HasKey(ps => ps.Id)
            // e.HasOne(
        }
    }
}
