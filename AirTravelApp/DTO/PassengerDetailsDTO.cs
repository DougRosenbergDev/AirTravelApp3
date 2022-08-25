using AirTravelApp.Models;

namespace AirTravelApp.DTO
{
    public class PassengerDetailsDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Job { get; set; }
        public int Age { get; set; }
        public List<Flight> flights { get; set; }

    }
}
