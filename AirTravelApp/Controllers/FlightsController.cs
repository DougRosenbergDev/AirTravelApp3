﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirTravelApp.Data;
using AirTravelApp.Models;
using AirTravelApp.DTO;

namespace AirTravelApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly FlightDbContext _context;
        private readonly ILogger<FlightsController> _logger;

        public FlightsController(ILogger<FlightsController> logger, FlightDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Flights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> GetFlights()
        {
          if (_context.Flights == null)
          {
              return NotFound();
          }
            return await _context.Flights.ToListAsync();
        }

        // GET: api/Flights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlight(int id)
        {
          if (_context.Flights == null)
          {
              return NotFound();
          }
            var flight = await _context.Flights.FindAsync(id);

            if (flight == null)
            {
                return NotFound();
            }
            var bookings = await _context.Passengers.Where(p => p.Bookings.Where(b => b.FlightId == flight.Id).Any()).ToListAsync();
            var flightDTO = new FlightDetailsDTO
            {
                Id = flight.Id,
                FlightNumber = flight.FlightNumber,
                DepartureDate = flight.DepartureDate,
                ArrivalDate = flight.ArrivalDate,
                DepartureTime = flight.DepartureDate,
                ArrivalTime = flight.ArrivalTime,
                DepartureAirport = flight.DepartureAirport,
                ArrivalAirport = flight.ArrivalAirport,
                PassengerLimit = flight.PassengerLimit,

                passengers = bookings

            };

            return Ok(flightDTO);
        }

        // PUT: api/Flights/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlight(int id, Flight flight)
        {
            if (id != flight.Id)
            {
                return BadRequest();
            }

            _context.Entry(flight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightExists(id))
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

        // POST: api/Flights
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Flight>> PostFlight(FlightDTO flightDto)
        {
          if (_context.Flights == null)
          {
              return Problem("Entity set 'FlightDbContext.Flights'  is null.");
          }
            // map our DTO
            // step 1: create a new flight object
            var flight = new Flight()
            {
                // pass fields, or create an empty contstructor, then create an overloaded constructor where we can pass
                // an instance of a FlightDto and it would return a new Flight instance
                FlightNumber = flightDto.FlightNumber,
                DepartureDate = flightDto.DepartureDate,
                ArrivalDate = flightDto.ArrivalDate,
                DepartureTime = flightDto.DepartureTime,
                ArrivalTime = flightDto.ArrivalTime,
                DepartureAirport = flightDto.DepartureAirport,
                ArrivalAirport = flightDto.ArrivalAirport,
                PassengerLimit = flightDto.PassengerLimit,
                //AppearsOnFlights = new List<BookedFlight>()
            };
            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlight", new { id = flight.Id }, flight);
        }

        // DELETE: api/Flights/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            if (_context.Flights == null)
            {
                return NotFound();
            }
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FlightExists(int id)
        {
            return (_context.Flights?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
