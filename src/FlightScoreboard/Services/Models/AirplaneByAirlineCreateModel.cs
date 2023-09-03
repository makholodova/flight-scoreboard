namespace FlightScoreboard.Services.Models;

public class AirplaneByAirlineCreateModel
{
	public int SerialNumber { get; set; }
	public int AirlineId { get; set; }
	public int AirplaneId { get; set; }
}