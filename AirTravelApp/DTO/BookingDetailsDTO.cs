using AirTravelApp.Models;

namespace AirTravelApp.DTO
{
    public class BookingDetailsDTO
    {
        public int Id { get; set; }
        //public string Name { get; set; }
        //public string? Description { get; set; }
        public int PassengerCount { get; set;  }
        public int DreamFlightCount { get; set; }

        public List<Passenger> Purchasers { get; set; }
        public List<Passenger> Dreams { get; set; }
        public List<Flight> Flights { get; set; }

    }
}
