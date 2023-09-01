namespace FlightScoreboard.DateBase;

public class Pilot
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string SurName { get; set; }
	public int Age { get; set; }

	public int AirlineId { get; set; }
	public virtual Airline Airline { get; set; }

	public virtual List<Flight> Flights { get; set; }
}