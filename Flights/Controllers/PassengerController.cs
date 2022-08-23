using Flights.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Flights.ReadModels;
using Flights.Domain.Entities;

namespace Flights.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class PassengerController : ControllerBase
	{

		static private IList<Passanger> Passengers = new List<Passanger>();

		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]

		public IActionResult Register(NewPassDto dto)
		{
			Passengers.Add
			(
			new Passanger
				(
				dto.Email,
				dto.FirstName,
				dto.LastName,	
				dto.gender
				)
				
			);
			System.Diagnostics.Debug.WriteLine("Count of passengers: {0}", Passengers.Count);
			return CreatedAtAction(nameof(Find), new {email = dto.Email});
		}

		[HttpGet("{email}")]
		public ActionResult<PassangerRm> Find(string email)
		{
			var passenger = Passengers.FirstOrDefault(p => p.Email == email);

			if(passenger == null)
				return NotFound();

			var rm = new PassangerRm(
				passenger.Email,
				passenger.FirstName,
				passenger.LastName,
				passenger.gender);

			return Ok(passenger);

		}
	}
}
