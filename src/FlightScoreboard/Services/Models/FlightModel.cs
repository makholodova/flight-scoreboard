namespace FlightScoreboard.Services.Models;

public class FlightModel
{
	public int Id { get; set; }
	public DateTime Time { get; set; }
	public int FromCityId { get; set; }
	public int ToCityId { get; set; }
	public int PilotId { get; set; }
	public int AirlineId { get; set; }
	public int AirlineAirplaneId { get; set; }
}