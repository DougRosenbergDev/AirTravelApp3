using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirTravelApp.Data;
using AirTravelApp.Models;
using AirTravelApp.DTO;

namespace AirTravelApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly FlightDbContext _context;
        private readonly ILogger<BookingsController> _logger;


        public BookingsController(ILogger<BookingsController> logger, FlightDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookedFlights()
        {
          if (_context.BookedFlights == null)
          {
              return NotFound();
          }
            return await _context.BookedFlights.ToListAsync();
        }

        // GET: api/Bookings/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<BookingDetailsDTO>> GetBooking(int id)
        //{
        //  if (_context.BookedFlights == null)
        //  {
        //      return NotFound();
        //  }
        //    var booking = await _context.BookedFlights.FindAsync(id);

        //    if (booking == null)
        //    {
        //        return NotFound();
        //    }

        //    //var flights = await _context.Flights.Where(p => p.AppearsOnFlights.Where(aof => aof.Id == booking.Id).Any()).ToListAsync();
        //    //var passengers = await _context.Passengers.Where(p => p.Bookings.Where(aof => aof.Id == booking.Id).Any()).ToListAsync();
        //    var flight = await _context.Flights.FirstOrDefaultAsync(f => f.Id == booking.FlightId);
        //    var passenger = await _context.Passengers.FirstOrDefaultAsync(f => f.Id == booking.PassengerId);
        //    if (flight == null || passenger == null)
        //    {
        //        return Problem("flight or passenger does not exist");
        //    }


        //    var pdDTO = new Booking
        //    {
        //        Id = booking.Id,
        //        FlightId = booking.FlightId,
        //        PassengerId = booking.PassengerId,
        //        Passenger =  passenger,
        //        Flight = flight

        //        //Name = booking.Name,
        //        //Description = booking.Description,
        //        //PassengerCount = booking.PassengerCount,
        //        //Purchasers = purchasers,
        //        //Dreams = dreams,
        //        //Flights = flights

        //    };

        //    _context.BookedFlights.Add(booking);
        //    passenger.Bookings.Add(booking);
        //    flight.AppearsOnFlights.Add(booking);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetBooking", new {id = booking.Id}, pdDTO);
        //}

        //// PUT: api/Bookings/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBooking(int id, Booking booking)
        //{
        //    if (id != booking.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(booking).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BookingExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(BookingDTO dto)
        {

            if (_context.BookedFlights == null)
            {
                return NotFound();
            }
            
            //var flights = await _context.Flights.Where(p => p.AppearsOnFlights.Where(aof => aof.Id == booking.Id).Any()).ToListAsync();
            //var passengers = await _context.Passengers.Where(p => p.Bookings.Where(aof => aof.Id == booking.Id).Any()).ToListAsync();
            var flight = await _context.Flights.FirstOrDefaultAsync(f => f.Id == dto.FlightId);
            var passenger = await _context.Passengers.FirstOrDefaultAsync(f => f.Id == dto.PassengerId);
            if (flight == null || passenger == null)
            {
                return Problem("flight or passenger does not exist");
            }


            var pdDTO = new Booking
            {
                FlightId = dto.FlightId,
                PassengerId = dto.PassengerId,
                ConfirmationNumber = dto.ConfirmationNumber,
                Passenger = passenger,
                Flight = flight

                //Name = booking.Name,
                //Description = booking.Description,
                //PassengerCount = booking.PassengerCount,
                //Purchasers = purchasers,
                //Dreams = dreams,
                //Flights = flights

            };

            _context.BookedFlights.Add(pdDTO);
            passenger.Bookings.Add(pdDTO);
            flight.AppearsOnFlights.Add(pdDTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = pdDTO.Id }, pdDTO);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            if (_context.BookedFlights == null)
            {
                return NotFound();
            }
            var booking = await _context.BookedFlights.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            if (booking.Flight != null)
            {
                booking.Flight.AppearsOnFlights.Remove(booking);  
            }
            if (booking.Passenger != null)
            {
                booking.Passenger.Bookings.Remove(booking);

            }
            _context.BookedFlights.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(int id)
        {
            return (_context.BookedFlights?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
