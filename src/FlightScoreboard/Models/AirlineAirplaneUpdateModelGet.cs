using System.Collections.Generic;
using FlightScoreboardData.Services.Models;

namespace FlightScoreboard.Models;

public class AirlineAirplaneUpdateModelGet
{
	public AirlineAirplaneModel Airplane { get; set; }
	public List<AirlineShortInfoModel> Airlines { get; set; }
	public List<AirplaneModel> Airplanes { get; set; }
}