using AirTravelApp.DTO;

namespace AirTravelApp.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int ConfirmationNumber { get; set; }
        public int FlightId { get; set; }
        public int PassengerId { get; set; }

        // error fixing
        

        // Not stored property
        public int PassengerCount => Purchasers?.Count ?? 0;
        public int DreamFlightCount => Dreams?.Count ?? 0;

        // Navigation Properties
        public virtual ICollection<BookedFlight> Flights { get; set; }

        public virtual ICollection<PurchasedFlight> Purchasers { get; set; }

        public virtual ICollection<DreamFlight> Dreams { get; set; }

        public Booking() { }

        public Booking(BookingDTO dto) {
            this.ConfirmationNumber = dto.ConfirmationNumber;
            this.FlightId = dto.FlightId;
            this.PassengerId = dto.PassengerId;
            this.Flights = new List<BookedFlight>();
            this.Purchasers = new List<PurchasedFlight>();
            this.Dreams = new List<DreamFlight>();
        }

    }
}
