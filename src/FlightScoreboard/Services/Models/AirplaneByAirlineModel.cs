namespace FlightScoreboard.Services.Models;

public class AirplaneByAirlineModel
{
	public int Id { get; set; }
	public int SerialNumber { get; set; }
	public int AirlineId { get; set; }
	public int AirplaneId { get; set; }
}