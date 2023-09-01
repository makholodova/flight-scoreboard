namespace FlightScoreboard.DateBase;

public class Airline
{
	public int Id { get; set; }
	public string Name { get; set; }

	public virtual List<Pilot> Pilots { get; set; }
	public virtual List<Flight> Flights { get; set; }
	public virtual List<AirplaneByAirline> AirplaneByAirlines { get; set; }
}