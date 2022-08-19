using AirTravelApp.DTO;
using System.Text.Json.Serialization;

namespace AirTravelApp.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int ConfirmationNumber { get; set; }
        public int FlightId { get; set; }
        public int PassengerId { get; set; }
        [JsonIgnore]
        public virtual Flight Flight { get; set; }
        [JsonIgnore]
        public virtual Passenger Passenger { get; set; }


        public Booking() { }

        public Booking(BookingDTO dto) {
            this.ConfirmationNumber = dto.ConfirmationNumber;
            this.FlightId = dto.FlightId;
            this.PassengerId = dto.PassengerId;
            
        }

    }
}
