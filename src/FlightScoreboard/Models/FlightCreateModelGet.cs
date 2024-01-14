using System;
using System.Collections.Generic;
using FlightScoreboardData.Services.Models;

namespace FlightScoreboard.Models;

public class FlightCreateModelGet
{
	public DateTime DepartureTime { get; set; }
	public DateTime ArrivalTime { get; set; }
	public List<AirlineShortInfoModel> Airlines { get; set; }
	public List<AirlineAirplaneShortModel> Airplanes { get; set; }
	public List<PilotIndexModel> Pilots { get; set; }
	public List<CityModel> Cities { get; set; }
}