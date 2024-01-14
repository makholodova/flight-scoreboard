using System;
using System.Collections.Generic;
using FlightScoreboardData.Services.Models;

namespace FlightScoreboard.Models;

public class ScoreboardArrivalIndexIpModel
{
	public DateTime? DateTime { get; set; }
	public int? CityId { get; set; }
	public List<CityModel> Cities { get; set; }
	public List<ScoreboardArrivalIndexModel> Flights { get; set; }
}