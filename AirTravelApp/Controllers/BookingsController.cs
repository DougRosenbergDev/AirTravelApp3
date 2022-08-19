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
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDetailsDTO>> GetBooking(int id)
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

            var purchasers = await _context.Passengers.Where(p => p.PurchasedFlights.Where(pf => pf.BookingId == booking.Id).Any()).ToListAsync();
            var dreams = await _context.Passengers.Where(p => p.DreamFlights.Where(pf => pf.BookingId == booking.Id).Any()).ToListAsync();
            var flights = await _context.Flights.Where(p => p.AppearsOnFlights.Where(aof => aof.BookingId == booking.Id).Any()).ToListAsync();

            var pdDTO = new BookingDetailsDTO
            {
                Id = booking.Id,
                //Name = booking.Name,
                //Description = booking.Description,
                PassengerCount = booking.PassengerCount,
                DreamFlightCount = booking.DreamFlightCount,
                Purchasers = purchasers,
                Dreams = dreams,
                Flights = flights

            };

            return Ok(pdDTO);
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(BookingDTO dto)
        {
          if (_context.BookedFlights == null)
          {
              return Problem("Entity set 'FlightDbContext.BookedFlights'  is null.");
          }
            var booking = new Booking(dto);
            _context.BookedFlights.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
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
