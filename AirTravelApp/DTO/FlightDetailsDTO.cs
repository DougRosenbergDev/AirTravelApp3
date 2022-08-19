using AirTravelApp.Models;

namespace AirTravelApp.DTO
{
    public class FlightDetailsDTO
    {
        public int Id { get; set; }
        public int FlightNumber { get; set; }
        public string DepartureDate { get; set; }
        public string ArrivalDate { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public int PassengerLimit { get; set; }
        public List<Passenger> passengers { get; set; }
    }
}
