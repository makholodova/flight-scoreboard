﻿namespace FlightScoreboard.DateBase;

public class Airplane
{
	public int Id { get; set; }
	public string Model { get; set; }

	public virtual List<AirlineAirplane> AirlineAirplanes { get; set; }
}