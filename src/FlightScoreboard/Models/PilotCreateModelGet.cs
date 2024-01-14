using System.Collections.Generic;
using FlightScoreboardData.Services.Models;

namespace FlightScoreboard.Models;

public class PilotCreateModelGet
{
	public List<AirlineShortInfoModel> Airlines { get; set; }
}