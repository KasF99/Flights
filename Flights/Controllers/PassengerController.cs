using Flights.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Flights.ReadModels;

namespace Flights.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class PassengerController : ControllerBase
	{

		static private IList<NewPassDto> Passengers = new List<NewPassDto>();

		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]

		public IActionResult Register(NewPassDto dto)
		{
			Passengers.Add(dto);
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
