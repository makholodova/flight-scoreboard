﻿using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Models;

public class AirlineAirplaneIndexModel
{
	public List<AirlineAirplaneShortModel> Airplanes { get; set; }
	public int AirlineId { get; set; }
}