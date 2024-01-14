using System.Collections.Generic;

namespace FlightScoreboardData.DateBase;

public class Airline
{
	public int Id { get; set; }
	public string Name { get; set; }

	public virtual List<Pilot> Pilots { get; set; }
	public virtual List<Flight> Flights { get; set; }
	public virtual List<AirlineAirplane> AirlineAirplanes { get; set; }
}