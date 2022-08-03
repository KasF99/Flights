namespace Flights.ReadModels
{
	public record FlightRm
	(
		Guid Id,
		string Airline,
		string Place,
		TimePlaceRm Departure,
		TimePlaceRm Arrival,
		int RemainingNumberOfSeats
	);
}
