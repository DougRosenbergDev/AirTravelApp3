namespace AirTravelApp.Models
{
    public class Passenger
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Job { get; set; }
        public int Age { get; set; }

        // Navigation Properties
        // in line 14 is it Bookedflights or PurchasedFlights?
        public virtual ICollection<PurchasedFlight> PurchasedFlights { get; set; }
        public virtual ICollection<DreamFlight> DreamFlights { get; set; }
        
    }
}
