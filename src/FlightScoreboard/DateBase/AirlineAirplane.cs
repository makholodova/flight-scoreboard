namespace FlightScoreboard.DateBase;

public class AirlineAirplane
{
	public int Id { get; set; }
	public int SerialNumber { get; set; }

	public int AirlineId { get; set; }
	public virtual Airline Airline { get; set; }

	public int AirplaneId { get; set; }
	public virtual Airplane Airplane { get; set; }

	public virtual List<Flight> Flights { get; set; }
}