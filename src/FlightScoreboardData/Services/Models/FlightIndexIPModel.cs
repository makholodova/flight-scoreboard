using System.Collections.Generic;
using FlightScoreboardData.Services.Models;

namespace FlightScoreboardData.Services.Models;

public class FlightIndexIpModel
{
	public FlightIndexFilterModel Flight { get; set; }
	public List<FlightIndexModel> Flights { get; set; }
	public List<AirlineAirplaneShortModel> Airplanes { get; set; }
	public List<PilotIndexModel> Pilots { get; set; }
	public List<CityModel> Cities { get; set; }
	public List<AirlineModel> Airlines { get; set; }
}