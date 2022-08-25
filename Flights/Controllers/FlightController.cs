using Flights.Domain.Entities;
using Flights.Dtos;
using Flights.ReadModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Flights.Data;
using Flights.Domain.Errors;

namespace Flights.Controllers
{
	[ApiController]
	[Route("[controller]")]


	public class FlightController : ControllerBase
	{

		private readonly ILogger<FlightController> _logger;

		private Entities _entities;
	public FlightController(ILogger<FlightController> logger, Entities entities)
		{
			_logger = logger;
			_entities = entities;
		}


		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(typeof(IEnumerable<FlightRm>), 200)]

		[HttpGet]
		public IEnumerable<FlightRm> Search()
		{
			var flightRmList = _entities.Flights.Select(flight => new FlightRm(
				flight.Id,
				flight.Airline,
				flight.Price,
				new TimePlaceRm(flight.Departure.Place, flight.Departure.Time),
				new TimePlaceRm(flight.Arrival.Place, flight.Arrival.Time),
				flight.RemainingNumberOfSeats));

		

		return flightRmList;
		}


		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(typeof(FlightRm),200)]
		[HttpGet("{id}")]

		public ActionResult<FlightRm> FindOrNull(Guid id)
		{
			var flight = _entities.Flights.SingleOrDefault(f => f.Id == id);

			if (flight == null)
				return NotFound();

			var readModel = new FlightRm(
				flight.Id,
				flight.Airline,
				flight.Price,
				new TimePlaceRm(flight.Departure.Place.ToString(), flight.Departure.Time),
				new TimePlaceRm(flight.Arrival.Place.ToString(), flight.Arrival.Time),
				flight.RemainingNumberOfSeats);
		
			return Ok(readModel);	
		}

		[HttpPost]
		[ProducesResponseType(400)] //client side error
		[ProducesResponseType(500)] //mising data 
		[ProducesResponseType(404)] //couldnt find the flight
		[ProducesResponseType(200)]

		public IActionResult Book(BookDto dto)
		{
			System.Diagnostics.Debug.WriteLine($"Booking new flight {dto.FlightId}");

			var flight = _entities.Flights.SingleOrDefault(f => f.Id == dto.FlightId);

			if(flight == null)
				return NotFound();

			var error = flight.MakeBooking(dto.PassengerEmail, dto.NumberOfSeats);

			if (error is OverBookError)
				return Conflict(new { message = "Not enough seats." });
			_entities.SaveChanges();	

			return CreatedAtAction(nameof(FindOrNull), new { id = dto.FlightId });

		}
	}
}