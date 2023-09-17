namespace FlightScoreboard.Services.Models;

public class AirlineAirplaneShortModel
{
	public int Id { get; set; }
	public int SerialNumber { get; set; }
	public int AirlineId { get; set; }
	public string AirlineName { get; set; }
	public int AirplaneId { get; set; }
	public string AirplaneModel { get; set; }
}