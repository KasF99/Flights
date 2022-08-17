namespace Flights.Dtos
{
	public record NewPassDto(
		string Email,
		string FirstName,
		string LastName,
		bool gender);
}
