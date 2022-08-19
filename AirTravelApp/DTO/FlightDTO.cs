namespace AirTravelApp.DTO
{
    public class FlightDTO
    {
        // this class is 1-1 with the Flight class
        // when you create a Flight, there willn't be an ID
        
        public int FlightNumber { get; set; }
        public string DepartureDate { get; set; }
        public string ArrivalDate { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public int PassengerLimit { get; set; }

    }
}
