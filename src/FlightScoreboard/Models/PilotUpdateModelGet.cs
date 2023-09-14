using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Models;

public class PilotUpdateModelGet
{
	public PilotModel Pilot { get; set; }
	public List<AirlineShortInfoModel> Airlines { get; set; }
}