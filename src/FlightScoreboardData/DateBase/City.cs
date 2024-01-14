using System.Collections.Generic;

namespace FlightScoreboardData.DateBase;

public class City
{
	public int Id { get; set; }
	public string Name { get; set; }

	public virtual List<Flight> FromFlights { get; set; }
	public virtual List<Flight> ToFlights { get; set; }
}