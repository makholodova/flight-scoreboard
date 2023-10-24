namespace FlightScoreboard.Services.Models;

public class FlightUpdateModel
{
	public int Id { get; set; }
	public DateTime DepartureTime { get; set; } 
	public DateTime ArrivalTime { get; set; }
	public int FromCityId { get; set; } 
	public int ToCityId { get; set; }
	public int PilotId { get; set; }
	public int AirlineId { get; set; }
	public int AirlineAirplaneId { get; set; }
	
	public DateTime ActualDepartureTime { get; set; }
	public DateTime ActualArrivalTime { get; set; }
	public DateTime CheckInStartTime { get; set; }
	public DateTime CheckInEndTime { get; set; }
	public DateTime BoardingStartTime { get; set; }
	public DateTime BoardingEndTime { get; set; }
	public string  NumberOfFlight { get; set; }
	public string  Gate { get; set; }
	public string  Terminal { get; set; }
}