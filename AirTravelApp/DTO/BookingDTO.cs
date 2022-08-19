namespace AirTravelApp.DTO
{
    public class BookingDTO
    {
        // inside of your controller, let's say your create method [HttpPOST]
        // it takes a Booking object
        // from the DB perspective, there needs to be an I first
        // The DTO allows you to only submit the necessary info
        // we don't necessarily want nullable properties
        // DTOs make things easier from the front-end
        public int ConfirmationNumber { get; set; }
        public int FlightId { get; set; }
        public int PassengerId { get; set; }
    }
}
