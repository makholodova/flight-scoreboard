using FlightScoreboardData.Services.Models;

namespace FlightScoreboard.Models;

public class AirlineAirplaneCreateModelGet
{
	public List<AirplaneModel> Airplanes { get; set; }
	public int AirlineId { get; set; }
}